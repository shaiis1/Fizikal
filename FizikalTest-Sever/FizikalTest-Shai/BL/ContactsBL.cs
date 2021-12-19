using FizikalTest_Shai.Models;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace FizikalTest_Shai.BL
{
    static public class ContactsBL
    {
        private static readonly string callsJson = "JSON/calls.json";
        private static readonly string citiesJson = "JSON/cities.json";
        private static readonly string customersJson = "JSON/customers.json";
        private static IMemoryCache _cache;

        public static CustomersList GetList()
        {
            var response = new CustomersList();
            var calls = new List<Calls>();
            var cities = new List<Cities>();
            var customers = new List<Customers>();
            #region Read From Json
            using (StreamReader r = new StreamReader(callsJson))
            {
                string json = r.ReadToEnd();
                calls = JsonConvert.DeserializeObject<List<Calls>>(json);
            }

            using (StreamReader r = new StreamReader(citiesJson))
            {
                string json = r.ReadToEnd();
                cities = JsonConvert.DeserializeObject<List<Cities>>(json);
            }

            using (StreamReader r = new StreamReader(customersJson))
            {
                string json = r.ReadToEnd();
                customers = JsonConvert.DeserializeObject<List<Customers>>(json);
            }
            #endregion
            var callsTimeMap = callsTimeMapping(calls);

            calcCallsTotalTime(customers, callsTimeMap);
            customers = customers.OrderByDescending(x => x.CallsTotalTime).ToList();
            response.CustomersLst = customers;
            response.CitiesLst = cities;

            return response;
        }

        private static void calcCallsTotalTime(List<Customers> customers, Dictionary<long,List<float>> calls)
        {
            foreach(var c in customers)
            {
                if(c.phoneNumbers.Length > 0)
                {
                    foreach(var number in c.phoneNumbers)
                    {
                        var numberTotalTime = 0.0f;
                        if (calls.ContainsKey(number))
                        {
                            var callsTimes = calls[number];
                            numberTotalTime = callsTimes.FirstOrDefault();
                            var allCalls = callsTimes.Skip(1);
                            foreach(var call in allCalls)
                            {
                                if(c.Calls == null)
                                {
                                    c.Calls = new List<Calls>();
                                }

                                c.Calls.Add(new Calls()
                                {
                                    callTime = TimeSpan.FromMinutes(double.Parse(call.ToString())),
                                    phoneNumber = number
                                });
                            }
                        }
                        c.CallsTotalTime += numberTotalTime;
                    }
                }
            }
        }

        private static Dictionary<long, List<float>> callsTimeMapping(List<Calls> calls)
        {
            var callsTimeDict = new Dictionary<long, List<float>>();
            foreach(var c in calls)
            {
                if (callsTimeDict.ContainsKey(c.phoneNumber))
                {
                    var time = c.callTime.TotalMinutes;
                    var currTotalTime = callsTimeDict[c.phoneNumber].FirstOrDefault();
                    currTotalTime += float.Parse(time.ToString());
                    callsTimeDict[c.phoneNumber][0] = currTotalTime;
                    callsTimeDict[c.phoneNumber].Add(float.Parse(time.ToString()));
                }

                else
                {
                    var time = c.callTime.TotalMinutes;
                    var totalTimeLst = new List<float>();
                    totalTimeLst.Add(float.Parse(time.ToString()));
                    callsTimeDict.Add(c.phoneNumber, totalTimeLst);
                }
            }

            return callsTimeDict;
        }
    }
}
