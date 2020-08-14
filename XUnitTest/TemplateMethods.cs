using System;
using System.Collections.Generic;
using System.Text;

namespace XUnitTest
{
    // This class is a collection of sample methods to be tested by using XUnit
    public class TemplateMethods
    {
        // You can add or customize your own method below 

        public static List<int> BubbleSort(List<int> data)
        {
            List<int> res = new List<int>(data);
            for(var i = 0; i < res.Count - 1; i++)
            {
                for(var j = 0; j < res.Count - i - 1; j++)
                {
                    if(res[j] > res[j + 1])
                    {
                        var temp = res[j];
                        res[j] = res[j + 1];
                        res[j + 1] = temp;
                    }
                }
            }
            return res;
        }

        public static bool SqrtMethodPrime(int n)
        {
            if (n <= 1)
                return false;

            bool res = true;
            for (var i = 2; i * i <= n && res; i++)
            {
                res = n % i != 0;
            }
            return res;
        }
    }
}
