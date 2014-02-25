using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using isukces.simple;

namespace isukces.sharppanel
{
    /*
    smartClass
    option NoAdditionalFile
    
    property Parked ParkedDomain[] Domeny zaparkowane
    	init new ParkedDomain[0]
    	preprocess value = value ?? new ParkedDomain[0];
    	OnChange parkedLoaded = true;
    
    property Addon AddonDomain[] Domeny dodatkowe
    	init new AddonDomain[0]
    	preprocess value = value ?? new AddonDomain[0];
    	OnChange addonLoaded = true;
    
    property Sub SubDomain[] Poddomeny
    	init new SubDomain[0]
    	preprocess value = value ?? new SubDomain[0];
    	OnChange subLoaded = true;
    
    property Root RootDomain[] Domeny główne
    	init new RootDomain[0]
    	preprocess value = value ?? new RootDomain[0];
    	OnChange rootLoaded = true;
    
    property ParkedLoaded bool Domeny zaparkowane załadowane
    	read only
    
    property AddonLoaded bool Domeny dodatkowe załadowane
    	read only
    
    property SubLoaded bool Poddomeny załadowane
    	read only
    
    property RootLoaded bool Domeny główne załadowane
    	read only
    
    property All ParkedDomain[] Domeny zaparkowane + dodatkowe + poddomeny
    	read only parked.Union(addon.OfType<ParkedDomain>()).Union(sub.OfType<ParkedDomain>()).ToArray();
    
    property All2 RootDomain[] Domeny zaparkowane + dodatkowe + poddomeny + główne
    	read only root.Union(parked.OfType<RootDomain>()).Union(addon.OfType<RootDomain>()).Union(sub.OfType<RootDomain>()).ToArray();
    smartClassEnd
    */

    /// <summary>
    /// Przechowuje listy domen
    /// </summary>
    public partial class DomainContainer
    {
        public override string ToString()
        {
            return string.Format("Domains r={0}, p={1}, a={2}, s={3}, total={4}",
                root.Length,
                parked.Length,
                addon.Length,
                sub.Length,
                root.Length + parked.Length + addon.Length + sub.Length);
        }
        public RootDomain Find(DomainName mydomain)
        {
            mydomain = mydomain.GetWithoutWWW();
            return All2.Where(q => mydomain.nameEquals(q.Domain)).FirstOrDefault();
        }

        public void RefreshSomeDomain(CPanel cPanel, ProgressReporter reporter)
        {
            ProgressReporter.Coalesce(ref reporter);
            reporter.Report(0, "Domeny zaparkowane");
            if (!parkedLoaded)
                Parked = cPanel.Call_ParkedDomain_List();
            reporter.Report(33, "Domeny dodatkowe");
            if (!addonLoaded)
                Addon = cPanel.Call_AddonDomain_List();
            reporter.Report(66, "Subdomeny");
            if (!subLoaded)
                Sub = cPanel.Call_SubDomain_List();
            reporter.Report(75, "Domeny główne");
            if (!rootLoaded)
                SetRootDomainsFromAllDomainsIncludingRoot(cPanel);
            reporter.Finish();
        }
        public RootDomain IncrementalDomainSearch(string domainName, bool forceReload, CPanel cPanel, ProgressReporter reporter)
        {
            ProgressReporter.Coalesce(ref reporter);
            reporter.Report(0, "Domeny zaparkowane");
            if (!parkedLoaded || forceReload)
                Parked = cPanel.Call_ParkedDomain_List();
            var f1 = Parked.Where(i => i.NameEquals(domainName));
            if (f1.Any())
                return f1.First();

            reporter.Report(33, "Domeny dodatkowe");
            if (!addonLoaded || forceReload)
                Addon = cPanel.Call_AddonDomain_List();
            var f2 = Addon.Where(i => i.NameEquals(domainName));
            if (f2.Any())
                return f2.First();

            reporter.Report(66, "Subdomeny");
            if (!subLoaded || forceReload)
                Sub = cPanel.Call_SubDomain_List();
            var f3 = Sub.Where(i => i.NameEquals(domainName));
            if (f3.Any())
                return f3.First();

            reporter.Report(75, "Domeny główne");
            if (!rootLoaded || forceReload)
            {
                SetRootDomainsFromAllDomainsIncludingRoot(cPanel);
                //var allNoRoot = All;
                //var allWithRoot = cPanel.Call_DomainLookup_GetDocRoots();
                // Root = DomainContainer.ComputeRootDomains(allNoRoot, allWithRoot);
            }
            var f4 = root.Where(i => i.NameEquals(domainName));
            if (f4.Any())
                return f4.First();

            reporter.Finish();
            return null;
        }
        /// <summary>
        /// Odnajduje nazwy domen innych niż parked,addons i subdomain, np. domeny głównej cpanela
        /// </summary>
        /// <param name="allNoRoot">parked,addons i subdomain</param>
        /// <param name="allWithRoot">domeny bazowe <see cref="Call_DomainLookup_GetBaseDomains">Call_DomainLookup_GetBaseDomains</see></param>
        /// <returns></returns>
        public static RootDomain[] ComputeRootDomains(ParkedDomain[] allNoRoot, RootDomain[] allWithRoot)
        {
            var o = allWithRoot.ToDictionary(q => q.Domain.ToLower(), q => q);
            foreach (ParkedDomain domain in allNoRoot)
            {
                var domainName = domain.Domain.ToLower();
                if (o.ContainsKey(domainName))
                    o.Remove(domainName);
            }
            var otherDomains = o.Values.ToArray();
            return otherDomains;
        }
        public RootDomain[] SetRootDomainsFromAllDomainsIncludingRoot(CPanel cpanel)
        {
            var allWithRoot = cpanel.Call_DomainLookup_GetDocRoots();
            return SetRootDomainsFromAllDomainsIncludingRoot(allWithRoot);
        }

        /// <summary>
        /// Ładuje wszystkie domeny
        /// </summary>
        /// <param name="cpanel"></param>
        public void Load(CPanel cpanel, ProgressReporter pr, bool skipCache)
        {
            var domains = cpanel.GetAllDomains2(pr, skipCache);
            Load(domains);
        }

        /// <summary>
        /// Ładuje wszystkie domeny
        /// </summary>
        /// <param name="domains">wszystkie domeny pobrane cpanel.GetAllDomains2()</param>
        private void Load(RootDomain[] domains)
        {
            Parked = domains.OfType<ParkedDomain>().Where(q => q.DomainType == DomainType.Parked).ToArray();
            Addon = domains.OfType<AddonDomain>().Where(q => q.DomainType == DomainType.Addon).ToArray();
            Sub = domains.OfType<SubDomain>().Where(q => q.DomainType == DomainType.Subdomain).ToArray();
            Root = domains.Where(q => q.DomainType == DomainType.Root).ToArray();
        }


        public RootDomain[] SetRootDomainsFromAllDomainsIncludingRoot(RootDomain[] AllDomainsIncludingRoot)
        {
            root = ComputeRootDomains(All, AllDomainsIncludingRoot);
            return root;
        }

        /// <summary>
        /// Tworzy tablicę składającą się z podanej domeny i domeny techniczej o ile istnieje
        /// </summary>
        /// <param name="domain"></param>
        /// <returns></returns>
        public RootDomain[] GetDomainArray(RootDomain domain)
        {
            List<RootDomain> domains1 = new List<RootDomain>();
            domains1.Add(domain);
            var t = GetTechDomain(domain);
            if (t != null)
                domains1.Add(domain);
            return domains1.ToArray();
            // copier.TestURL = domains1.Select(q => "http://" + q).Distinct().ToArray();
        }

        /// <summary>
        /// Zwraca domenę techniczną zwązaną z tą domeną
        /// </summary>
        /// <param name="domains"></param>
        /// <param name="domain"></param>
        /// <returns></returns>
        public string GetTechDomain(RootDomain domain)
        {
            switch (domain.DomainType)
            {
                case DomainType.Addon:
                    return ((AddonDomain)domain).FullSubdomain;
                case DomainType.Parked:
                    var xroot = root.Where(q => q.DomainType == DomainType.Root).FirstOrDefault();
                    if (xroot != null)
                        return xroot.Domain;
                    return null;
            }
            return null;
        }
    }
}


// -----:::::##### smartClass embedded code begin #####:::::----- generated 2012-08-13 11:02
// File generated automatically ver 2012-05-05 12:00
// SmartClass2010Net35, Version=1.0.1.21, Culture=neutral, PublicKeyToken=4aa5e0e1662c5135
namespace isukces.sharppanel
{
    public partial class DomainContainer
    {
        /*
        /// <summary>
        /// Tworzy instancję obiektu
        /// </summary>
        public DomainContainer()
        {
        }

        Przykłady użycia

        implement INotifyPropertyChanged
        implement INotifyPropertyChanged_Passive
        implement ToString ##Parked## ##Addon## ##Sub## ##Root## ##ParkedLoaded## ##AddonLoaded## ##SubLoaded## ##RootLoaded## ##All## ##All2##
        implement ToString Parked=##Parked##, Addon=##Addon##, Sub=##Sub##, Root=##Root##, ParkedLoaded=##ParkedLoaded##, AddonLoaded=##AddonLoaded##, SubLoaded=##SubLoaded##, RootLoaded=##RootLoaded##, All=##All##, All2=##All2##
        implement equals Parked, Addon, Sub, Root, ParkedLoaded, AddonLoaded, SubLoaded, RootLoaded, All, All2
        implement equals *
        implement equals *, ~exclude1, ~exclude2
        */
        #region Constants
        /// <summary>
        /// Nazwa własności Parked; Domeny zaparkowane
        /// </summary>
        public const string PROPERTYNAME_PARKED = "Parked";
        /// <summary>
        /// Nazwa własności Addon; Domeny dodatkowe
        /// </summary>
        public const string PROPERTYNAME_ADDON = "Addon";
        /// <summary>
        /// Nazwa własności Sub; Poddomeny
        /// </summary>
        public const string PROPERTYNAME_SUB = "Sub";
        /// <summary>
        /// Nazwa własności Root; Domeny główne
        /// </summary>
        public const string PROPERTYNAME_ROOT = "Root";
        /// <summary>
        /// Nazwa własności ParkedLoaded; Domeny zaparkowane załadowane
        /// </summary>
        public const string PROPERTYNAME_PARKEDLOADED = "ParkedLoaded";
        /// <summary>
        /// Nazwa własności AddonLoaded; Domeny dodatkowe załadowane
        /// </summary>
        public const string PROPERTYNAME_ADDONLOADED = "AddonLoaded";
        /// <summary>
        /// Nazwa własności SubLoaded; Poddomeny załadowane
        /// </summary>
        public const string PROPERTYNAME_SUBLOADED = "SubLoaded";
        /// <summary>
        /// Nazwa własności RootLoaded; Domeny główne załadowane
        /// </summary>
        public const string PROPERTYNAME_ROOTLOADED = "RootLoaded";
        /// <summary>
        /// Nazwa własności All; Domeny zaparkowane + dodatkowe + poddomeny
        /// </summary>
        public const string PROPERTYNAME_ALL = "All";
        /// <summary>
        /// Nazwa własności All2; Domeny zaparkowane + dodatkowe + poddomeny + główne
        /// </summary>
        public const string PROPERTYNAME_ALL2 = "All2";
        #endregion Constants

        #region Methods
        #endregion Methods

        #region Properties
        /// <summary>
        /// Domeny zaparkowane
        /// </summary>
        public ParkedDomain[] Parked
        {
            get
            {
                return parked;
            }
            set
            {
                value = value ?? new ParkedDomain[0];
                if (value == parked) return;
                parked = value;
                parkedLoaded = true;
            }
        }
        private ParkedDomain[] parked = new ParkedDomain[0];
        /// <summary>
        /// Domeny dodatkowe
        /// </summary>
        public AddonDomain[] Addon
        {
            get
            {
                return addon;
            }
            set
            {
                value = value ?? new AddonDomain[0];
                if (value == addon) return;
                addon = value;
                addonLoaded = true;
            }
        }
        private AddonDomain[] addon = new AddonDomain[0];
        /// <summary>
        /// Poddomeny
        /// </summary>
        public SubDomain[] Sub
        {
            get
            {
                return sub;
            }
            set
            {
                value = value ?? new SubDomain[0];
                if (value == sub) return;
                sub = value;
                subLoaded = true;
            }
        }
        private SubDomain[] sub = new SubDomain[0];
        /// <summary>
        /// Domeny główne
        /// </summary>
        public RootDomain[] Root
        {
            get
            {
                return root;
            }
            set
            {
                value = value ?? new RootDomain[0];
                if (value == root) return;
                root = value;
                rootLoaded = true;
            }
        }
        private RootDomain[] root = new RootDomain[0];
        /// <summary>
        /// Domeny zaparkowane załadowane; własność jest tylko do odczytu.
        /// </summary>
        public bool ParkedLoaded
        {
            get
            {
                return parkedLoaded;
            }
        }
        private bool parkedLoaded;
        /// <summary>
        /// Domeny dodatkowe załadowane; własność jest tylko do odczytu.
        /// </summary>
        public bool AddonLoaded
        {
            get
            {
                return addonLoaded;
            }
        }
        private bool addonLoaded;
        /// <summary>
        /// Poddomeny załadowane; własność jest tylko do odczytu.
        /// </summary>
        public bool SubLoaded
        {
            get
            {
                return subLoaded;
            }
        }
        private bool subLoaded;
        /// <summary>
        /// Domeny główne załadowane; własność jest tylko do odczytu.
        /// </summary>
        public bool RootLoaded
        {
            get
            {
                return rootLoaded;
            }
        }
        private bool rootLoaded;
        /// <summary>
        /// Domeny zaparkowane + dodatkowe + poddomeny; własność jest tylko do odczytu.
        /// </summary>
        public ParkedDomain[] All
        {
            get
            {
                return parked.Union(addon.OfType<ParkedDomain>()).Union(sub.OfType<ParkedDomain>()).ToArray(); ;
            }
        }
        /// <summary>
        /// Domeny zaparkowane + dodatkowe + poddomeny + główne; własność jest tylko do odczytu.
        /// </summary>
        public RootDomain[] All2
        {
            get
            {
                return root.Union(parked.OfType<RootDomain>()).Union(addon.OfType<RootDomain>()).Union(sub.OfType<RootDomain>()).ToArray(); ;
            }
        }
        #endregion Properties

    }
}
