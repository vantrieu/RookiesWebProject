using System;
using System.Collections.Generic;
using System.Text;

namespace Web.ShareModels.ViewModels
{
    public class ProductVm
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public int Quantities { get; set; }

        public Int64 Price { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime UpdatedDate { get; set; }

        public string CategoryName { get; set; }

        public List<int> Rates { get; set; }

        public List<string> ProductFileImages { get; set; }

        public double RateAvg()
        {
            if (Rates.Count == 0)
                return 5;
            int sum = 0;
            foreach(int rate in Rates)
            {
                sum += rate;
            }
            return Math.Round((double)sum / Rates.Count, 1);
        }

        public string GetFirstImage()
        {
            return ProductFileImages[0];
        }
    }
}
