using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Session.Constants
{
    public static class Constants
    {
        public static DateTime DefaultEmptyDateForEvent = new DateTime(2020, 1, 1);

        public static DateTime DefaultEditDate = new DateTime(2014, 6, 2);

        public static string UchOtdHead = "Карасёва Т.Б.";

        public static Dictionary<int, List<string>> facultyGroups = new Dictionary<int, List<string>> {             
            { 1, new List<string> { "12 А", "13 А" } }, 
            { 2, new List<string> { "12 Б", "13 Б", "14 Б" } }, 
            { 3, new List<string> { "12 В0", "12 В", "13 В", "14 В" } },
            { 4, new List<string> { "12 Г", "13 Г", "14 Г" } }, 
            { 5, new List<string> { "12 Д", "13 Д", "14 Д" } },
            { 6, new List<string> { "12 Е", "13 Е", "14 Е" } }, 
            { 7, new List<string> { "12 У", "13 У", "14 У", "15 У" } },
            { 8, new List<string> { "12 Т", "13 Т", "14 Т"} },
            { 9, new List<string> { "12 Г(Н)", "13 Г(Н)"} }, 
            { 10, new List<string> { "12 Д(Н)", "13 Д(Н)"} }
        };

        public static List<string> facultyTitles = new List<string> { 
            "факультета математики и компьютерных наук", 
            "философского факультета",
            "химико-биологического факультета", 
            "экономического факультета", 
            "юридического факультета",
            "факультета международных отношений", 
            "факультета управления", 
            "факультета туризма", 
            "экономического факультета", 
            "юридического факультета"
        };

        public static Dictionary<int, string> HeadsOfFaculties = new Dictionary<int, string> { 
            {1, "Сидоров А.А."},         //"А"
            {2, "Стёпкина М.В."},        //"Б"
            {3, "Сидорина Н.Е."},        //"В" 
            {4, "Зиновьева О.Г."},       //"Г" 
            {5, "Никищенкова М.А."},     //"Д" 
            {6, "Сомова С.В."},          //"Е" 
            {7, "Спирина Т.В."},         //"У" 
            {8, "Соломина И.Ю."},        //"Т" 
            {9, "Зиновьева О.Г."},       //"ГН" 
            {10, "Никищенкова М.А."}     //"ДН" 
        };
    }
}
