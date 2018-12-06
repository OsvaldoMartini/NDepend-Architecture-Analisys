using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using Geo.Localization.Data.IRepository;
using Geo.Localization.Data.Utils;
using MySql.Data.MySqlClient;

namespace Geo.Localization.Data.Repository
{
    public class CompanySaleRepository : GenericRepository<CompanySaleEntity>, ICompanySaleRepository
    {
        /// <summary>
        /// Method responsible for simulating data load;
        /// </summary>
        /// <returns></returns>
        public List<CompanySaleEntity> GetCorpSaleByType(string typeCompany)
         {
             List<CompanyEntity> listCompanies = new List<CompanyEntity>()
             {
                 new CompanyEntity(){CompanyID = 1,CompanyType = "aero"},
                 new CompanyEntity(){CompanyID = 2,CompanyType = "toys"},
                 new CompanyEntity(){CompanyID = 3,CompanyType = "bags"},
                 new CompanyEntity(){CompanyID = 4,CompanyType = "metal"},
                 new CompanyEntity(){CompanyID = 5,CompanyType = "hats"},


             };

             var companies = new int[] { 1, 2, 3, 4, 5 };

            if (!string.IsNullOrEmpty(typeCompany) && typeCompany != "all")
             {
                 List<CompanyEntity> list = listCompanies.Where(v => v.CompanyType.Equals(typeCompany)).ToList();
                 if (list.Count == 0)
                     return new List<CompanySaleEntity>();

                 Array.Resize(ref companies, list.Count);
                 for (int x=0; x <= list.Count-1; x++)
                     companies[x] = list[x].CompanyID;
             }

             var deckCompanies = CreateShuffledDeck(companies);


            var months = new int[] { 1, 2, 3, 4, 5, 6 ,7, 8, 9, 10 ,11 ,12 };
             var deckMonths = CreateShuffledDeck(months);
             var years = new int[] { 2012, 2013, 2014, 2015, 2016, 2017, 2018 };
             var deckYears = CreateShuffledDeck(years);

             var sale = new int[] { 10, 11, 12, 13, 14, 15, 16, 17, 18 ,19, 22, 25 ,28};
             var deckSale = CreateShuffledDeck(sale);



            List<CompanySaleEntity> listCorpSaleByType = new List<CompanySaleEntity>();

             int loop = 35;
            while (loop > 0)
            {
                var monthRange = -1;
                if (deckMonths.Count > 0)
                    monthRange = deckMonths.Pop();
                else
                {
                    //Refil deckMonths
                    deckMonths= CreateShuffledDeck(months);
                    monthRange = deckMonths.Pop();
                }

                 var companyRange = -1;
                if (deckCompanies.Count > 0)
                     companyRange = deckCompanies.Pop();
                 else
                 {
                    //Refil deckCompanies
                     deckCompanies = CreateShuffledDeck(companies);
                     companyRange = deckCompanies.Pop();
                }

                var yearRange = -1;
                if (deckYears.Count > 0)
                    yearRange = deckYears.Pop();
                else
                {
                    //Refil deckCompanies
                    deckYears = CreateShuffledDeck(years);
                    yearRange = deckYears.Pop();
                }

                var saleRange = -1;
                if (deckSale.Count > 0)
                    saleRange = deckSale.Pop();
                else
                {
                    //Refil deckCompanies
                    deckSale = CreateShuffledDeck(sale);
                    saleRange = deckSale.Pop();
                }



                listCorpSaleByType.Add(new CompanySaleEntity()
                 {
                     CompanySaleID = 35 - loop + 1,
                     SaleYear = yearRange,
                     SaleMonth = System.Globalization.DateTimeFormatInfo.CurrentInfo.GetMonthName(monthRange),
                     TotalSale = saleRange,
                     DateCreated = DateTime.Now.AddYears(-5),
                     TCompany = listCompanies[companyRange-1]
                 });

                loop--;

            }

             return listCorpSaleByType;
        }

        private Stack<T> CreateShuffledDeck<T>(IEnumerable<T> values)
        {
            var rand = new Random();

            var list = new List<T>(values);
            var stack = new Stack<T>();
             
            while (list.Count > 0)
            {
                // Get the next item at random.
                var index = rand.Next(0, list.Count);
                var item = list[index];

                // Remove the item from the list and push it to the top of the deck.
                list.RemoveAt(index);
                stack.Push(item);
            }

            return stack;
        }

    }
}   