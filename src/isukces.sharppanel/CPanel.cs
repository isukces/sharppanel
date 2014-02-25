using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using isukces.simple;


namespace isukces.sharppanel
{
    public class CPanel : ApiFunctions
    {
        /// <summary>
        /// Tworzy instancję obiektu
        /// <param name="URL">URL bazowy cpanela</param>
        /// <param name="Username">Nazwa użytkownika</param>
        /// <param name="Password">hasło</param>
        /// </summary>
        public CPanel(string URL, string Username, string Password)
            : base(URL, Username, Password)
        {
        }




        /// <summary>
        /// Pobiera listę wszystkich domen, ale bez root
        /// </summary>
        /// <param name="percent_report">Raportuje postęp</param>
        /// <returns></returns>
        public ParkedDomain[] GetAllDomains(ProgressReporter percent_report, bool skipCache = false)
        {
            ParkedDomain[] all;
            ParkedDomain[] parkedDomains;
            AddonDomain[] addonDomains;
            SubDomain[] subDomains;

            ProgressReporter.Coalesce(ref percent_report);

            percent_report.Report(0, Polish.strDomenyZaparkowane);
            parkedDomains = Call_ParkedDomain_List(skipCache);
            percent_report.Report(33, Polish.strDomenyDodatkowe);
            addonDomains = Call_AddonDomain_List(skipCache);
            percent_report.Report(67, Polish.strSubdomeny);
            subDomains = Call_SubDomain_List(skipCache);
            percent_report.Finish();
            all = parkedDomains.Concat(addonDomains.OfType<ParkedDomain>()).Concat(subDomains.OfType<ParkedDomain>()).ToArray();
            return all;
        }

        /// <summary>
        /// Zwraca dokładnie wszystkie domeny
        /// </summary>
        /// <param name="percent_report"></param>
        /// <param name="skipCache"></param>
        /// <returns></returns>
        public RootDomain[] GetAllDomains2(ProgressReporter percent_report, bool skipCache = false)
        {
            ProgressReporter.Coalesce(ref percent_report);
            ParkedDomain[] allNoRoot;
            if (percent_report != null)
                allNoRoot = GetAllDomains(percent_report.GetScaled(0, 75), skipCache);
            else
                allNoRoot = GetAllDomains(null, skipCache);
            percent_report.Report(75, Polish.strDomenyGlowne);
            var allWithRoot = Call_DomainLookup_GetDocRoots();
            percent_report.Finish();
            var c = DomainContainer.ComputeRootDomains(allNoRoot, allWithRoot);
            return c.Union(allNoRoot).ToArray();
        }



        /// <summary>
        /// Dodaje domenę, automatycznie rozpoznaje czy to domena parkowana czy subdomena
        /// </summary>
        /// <param name="domainName">nazwa domeny</param>
        /// <param name="ask">Metoda, która zapytuje, kiedy potrzebna akcja użytkownika</param>
        /// <param name="pr">raport postępu</param>
        /// <param name="subDirectory">podkatalog dla Apache</param>
        /// <returns></returns>
        public AddDomainResult AddDomain(string domainName, ProgressReporter pr, ref DomainContainer dc, string subDirectory = null, AskDelegate ask = null)
        {
            if (ask == null)
                ask = (a, b) => b;
            double percentBareer = dc == null ? 60 : 10;
            ProgressReporter.Coalesce(ref pr);
            if (dc == null)
                dc = new DomainContainer();
            #region Test if domain already exists
            {
                using (var pr2 = pr.GetScaledAppend(0, percentBareer, Polish.strSprawdzanieDomenZainstalowanych))
                {
                    var found = dc.IncrementalDomainSearch(domainName, false, this, pr2);
                    if (found != null)
                        return new AddDomainResult(found, AddDomainStatus.AlreadyExists);
                }
            }
            #endregion


            using (var pr2 = pr.GetScaled(percentBareer, 100))
            {
                pr2.Report(0, Polish.strAnalizaMetodyDodaniaDomeny);
                ParkedDomain[] all = dc.All;
                dc.RefreshSomeDomain(this, null);
                // c.SetRootDomainsFromAllDomainsIncludingRoot(this);

                if (string.IsNullOrEmpty(subDirectory))
                    subDirectory = "/#" + domainName;
                if (!subDirectory.StartsWith("/"))
                    subDirectory = "/" + subDirectory;
                DomainName dn = domainName;

                pr2.Report(10, Polish.strInformacjaOZasobachSerwera);
                Statistics stat = Call_StatsBar();  ////////////////////////////
                #region Instalacja subdomeny o ile to możliwe
                if (stat.SubDomains.CanUse)
                    foreach (var subd in dn.GetParents(1, dn.Segments - 1))
                    {
                        IEnumerable<RootDomain> findParentDomain = dc.All2.Where(ii => ii.NameEquals(subd));
                        if (!findParentDomain.Any())
                            continue;
                        RootDomain parentDomain = findParentDomain.First();
                        if (parentDomain is SubDomain)
                        {
                            var rootDomain = (parentDomain as SubDomain).RootDomain;
                            var tt = all.Where(ii => ii.NameEquals(rootDomain));
                            if (tt.Any())
                                parentDomain = tt.First();
                        }
                        #region Instalacja subdomeny
                        {
                            var sub = domainName.Substring(0, domainName.Length - parentDomain.Domain.Length - 1);
                            var absoluteBaseDir = parentDomain.Dir;
                            if (dc.Root.Any())
                                absoluteBaseDir = dc.Root[0].Dir;
                            pr2.Report(20, Polish.strDodawaniePoddomeny);
                            Call_SubDomain_Add(absoluteBaseDir + subDirectory, false, sub, parentDomain.Domain); ////////// 
                            pr2.Report(60, Polish.strOczekiwanieNaListeDomen);
                            dc.Sub = Call_SubDomain_List(true);
                            var t = dc.Sub.Where(ii => ii.NameEquals(domainName)).FirstOrDefault(); ///////////////
                            pr2.Finish();
                            return new AddDomainResult(t,
                                  t == null ? AddDomainStatus.UnableToAddSubDomain : AddDomainStatus.SubDomainAdded);

                        }
                        #endregion
                    }
                #endregion
                #region Instalacja Addon o ile to możliwe
                {
                    if (stat.AddonDomains.CanUse)
                    {
                        pr2.Report(20, Polish.strOczekiwanieNaListeUzytkownikowFTP);
                        var ftpu = Call_Ftp_List();
                        string sftp = "";
                        int i = 0;
                        while (true)
                        {
                            sftp = dn.NameItems[0].Replace("-", "").ToLower();
                            if (i > 0) sftp += i.ToString();
                            if (!ftpu.Where(ii => ii.Name.ToLower() == sftp).Any())
                            {
                                // inne sprawdzenia tu mogą być
                                break;
                            }
                            i++;
                        }
                        pr2.Report(50, Polish.strDodawanieDomenyDodatkowej);
                        var result = Call_AddonDomain_Add("public_html" + subDirectory, domainName, sftp);
                        pr2.Report(65, Polish.strOczekiwanieNaListeDomen);
                        dc.Addon = Call_AddonDomain_List(true);
                        pr2.Report(80, Polish.strOczekiwanieNaListeDomen);
                        dc.Sub = Call_SubDomain_List(true);

                        var t = dc.Addon.Where(ii => ii.NameEquals(domainName)).FirstOrDefault();
                        return new AddDomainResult(t,
                            t == null ? AddDomainStatus.UnableToAddAddonDomain : AddDomainStatus.AddonDomainAdded);
                    }
                }
                #endregion
                {
                    // no to dodajemy nową domenę "główną" jako dodatkową
                    // var result = Call_AddonDomain_Add("public_html" + subDirectory, domainName, "niewiem4");
                    // FullSubdomain niewiem4.you.yourprovider.com
                    // niewiem4 jest też nazwą użytkownika FTP
                    // katalog = 
                    if (!stat.ParkedDomains.CanUse)
                        return new AddDomainResult(null, AddDomainStatus.UnableToAddAddonDomain);

                    bool doInstall = false;
                    if (stat.ParkedDomains.Count > 0)
                    {

                        string txt;
                        if (stat.AddonDomains.Limit > 0)
                            txt = Polish.strTwojSerwerNieUmozliwiaJuzZarejestrowanieKolejnejDomenyDodatkowej;
                        else
                            txt = string.Format(Polish.strDomena0MozeBycZainstalowanaTylkoJakoZaparkowanaANaTwoimSerwerzeIstniejeJuzInnaDomenaTegoTypu, domainName);
                        // +" może być zainstalowana tylko jako 'zaparkowana', a na Twoim serwerze istnieje już inna domena tego typu.";
                        txt += Polish.strCzyChceszNadpisacIstniejacyKatalogZDanymi
                            + "\r\n\r\n"
                            + Polish.strPodpowiedzJesliChceszZachowacIstniejaceDaneZrezygnujZInstalacjiWTejChwiliIPowiekszNajpierwPakietHostingowy;
                        doInstall = ask(txt, false);
                    }
                    else
                        doInstall = ask(
                            string.Format(Polish.strDomena0MozeBycZainstalowanaTylkoJakoZaparkowanaWKataloguGlownymSerwera, domainName)
                            + "\r\n"
                            + Polish.strInstalacjaInnychDomenTegoTypuNieBedzieWPrzyszlosciMozliwa
                            + " "
                            + Polish.strNieJestToProblemJesliNiePlanujeszInnychDomenLubPowiekszyszPakietHostingowy
                            + "\r\n"
                            + Polish.strCzyChceszZainstalowacDomeneZaparkowana, false);
                    if (!doInstall)
                        return new AddDomainResult(null, AddDomainStatus.UnableToAddAddonDomain);
                    pr2.Report(40, Polish.strDodawanieDomenyZaparkowanej);
                    var a = Call_ParkedDomain_Add(domainName);
                    pr2.Report(80, Polish.strOczekiwanieNaListeDomen);
                    dc.Parked = Call_ParkedDomain_List(true);
                    var t = dc.Parked.Where(ii => ii.NameEquals(domainName)).FirstOrDefault();
                    return new AddDomainResult(t,
                        t == null ? AddDomainStatus.UnableToAddSubDomain : AddDomainStatus.SubDomainAdded);
                }
            }
        }

    }
}
