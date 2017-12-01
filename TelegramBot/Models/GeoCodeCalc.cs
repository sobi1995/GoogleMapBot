using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Xml;

namespace TelegramBot.Models
{
    public static class GeoCodeCalc
    {
        public const double EarthRadiusInMiles = 3956.0;
        public const double EarthRadiusInKilometers = 6367.0;

        public static double ToRadian(double val) { return val * (Math.PI / 180); }
        public static double DiffRadian(double val1, double val2) { return ToRadian(val2) - ToRadian(val1); }

        public static double CalcDistance(TelegramBot.Models.LocationM Location1, TelegramBot.Models.LocationM Location2)
        {
            return CalcDistance(Location1, Location2, GeoCodeCalcMeasurement.Miles);
        }
        //dsfsdfsdfsdf
        //sdfsdfs
        //sdfbsdfhjsd
        //dsjhfkdjs
        //sdjgfsdgjfgds
        //ksdhfhsdgj
        public static double CalcDistance(TelegramBot.Models.LocationM Location1, TelegramBot.Models.LocationM Location2, GeoCodeCalcMeasurement m)
        {
            double radius = GeoCodeCalc.EarthRadiusInMiles;

            if (m == GeoCodeCalcMeasurement.Kilometers) { radius = GeoCodeCalc.EarthRadiusInKilometers; }
            return radius * 2 * Math.Asin(Math.Min(1, Math.Sqrt((Math.Pow(Math.Sin((DiffRadian(Location1.X, Location2.X)) / 2.0), 2.0) + Math.Cos(ToRadian(Location1.X)) * Math.Cos(ToRadian(Location2.X)) * Math.Pow(Math.Sin((DiffRadian(Location1.Y, Location2.Y)) / 2.0), 2.0)))));
        }

        public static void GetAddressFromLatLon(Decimal Latitude, Decimal Longitude)
        {
            string result = null;
            System.Net.WebClient webClient = new System.Net.WebClient();
            string s1 = Latitude.ToString().Replace("/", ".");
            string s2 = Longitude.ToString().Replace("/", "."); ;
            var apiUrl = "https://maps.googleapis.com/maps/api/geocode/xml?latlng=34.3339424133301,46.6876678466797&sensor=false";
            var searchRequest = "";
            webClient.Headers.Add("content-type", "application/x-www-form-urlencoded");
            result = webClient.UploadString(apiUrl, searchRequest);
            XmlDocument xbook = new XmlDocument();
            xbook.LoadXml(result);
            string CountryCode = string.Empty;
            string CountryName = string.Empty;
            string StateCode = string.Empty;
            string stateName = string.Empty;
            foreach (XmlNode node in xbook.DocumentElement.ChildNodes)
            {
                if (node.Name == "result")
                {
                    foreach (XmlNode node1 in node)
                    {
                        if (node1.Name == "address_component")
                        {
                            foreach (XmlNode node2 in node1)
                            {
                                if (node2.Name == "type" && node2.InnerText == "country")
                                {
                                    foreach (XmlNode node3 in node1)
                                    {
                                        if (node3.Name == "long_name")
                                        {
                                            CountryName = node3.InnerText;
                                        }
                                        else if (node3.Name == "short_name")
                                        {
                                            CountryCode = node3.InnerText;
                                        }
                                    }
                                }
                                if (node2.Name == "type" && node2.InnerText == "administrative_area_level_1")
                                {
                                    foreach (XmlNode node3 in node1)
                                    {
                                        if (node3.Name == "long_name")
                                        {
                                            stateName = node3.InnerText;
                                        }
                                        else if (node3.Name == "short_name")
                                        {
                                            StateCode = node3.InnerText;
                                        }
                                    }
                                }
                            }
                        }
                    }
                    break;
                }
            }


        }
         
    }

  
  
}
  

public enum GeoCodeCalcMeasurement : int
{
    Miles = 0,
    Kilometers = 1
}