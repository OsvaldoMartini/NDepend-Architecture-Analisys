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
    public class CompanySalesRepository : GenericRepository<CompanySalesEntity>, ICompanySalesRepository
    {
         /// <summary>
        /// Método responsável por simular a carga de dados;
        /// </summary>
        /// <returns></returns>
        public IEnumerable<CompanySalesEntity> GetCorpSalesByType(string typeCompany)
         {
             List<CompanyEntity> listCompanies = new List<CompanyEntity>()
             {
                 new CompanyEntity(){CompanyID = 1,CompanyType = "aero"},
                 new CompanyEntity(){CompanyID = 2,CompanyType = "toys"},
                 new CompanyEntity(){CompanyID = 3,CompanyType = "bags"},
                 new CompanyEntity(){CompanyID = 4,CompanyType = "metal"},
                 new CompanyEntity(){CompanyID = 5,CompanyType = "hats"},


             };


             var months = new int[] { 0, 1, 2, 3, 4, 5, 6 ,7, 8, 9, 10 ,11 ,12 };
             var deck = CreateShuffledDeck(months);
             var companies = new int[] { 0, 1, 2, 3, 4};
             var deckCompanies = CreateShuffledDeck(companies);
             var years = new int[] { 2012, 2013, 2014, 2015, 2016, 2017, 2018 };
             var deckYears = CreateShuffledDeck(years);

            List<CompanySalesEntity> listCorpSalesByType = new List<CompanySalesEntity>();

            while (deck.Count > 0)
             {
                 var monthRange = deck.Pop();
                 var companyRange = deckCompanies.Pop();
                  var yearRange = deckYears.Pop();
                 listCorpSalesByType.Add(new CompanySalesEntity()
                 {
                     CompanySalesID = 1,
                     Year = yearRange,
                     Month = System.Globalization.DateTimeFormatInfo.CurrentInfo.GetAbbreviatedMonthName(monthRange),
                     Sales = 17,
                     DateCreated = DateTime.Now.AddYears(-5),
                     TCompany = listCompanies[companyRange]
                 });

             }

             return listCorpSalesByType;
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