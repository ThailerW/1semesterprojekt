using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SynsPunkt_ChatBot.Services
{
    public class GetBrilleList
    {
        public List<Models.Briller> GetAllBrilleDummyData()
        {
            //Dummy data via chatgpt
            List<Models.Briller> briller = new List<Models.Briller>();

            Models.Briller brille1 = new Models.Briller("Stylish Shades", 10.10, 12.14, "Grøn", 5.12, false);
            briller.Add(brille1);

            Models.Briller brille2 = new Models.Briller("Cool Spectacles", 10.11, 13.14, "Hvid", 4.5, true);
            briller.Add(brille2);

            Models.Briller brille3 = new Models.Briller("Retro Frames", 9.5, 11.5, "Sort", 4.8, false);
            briller.Add(brille3);

            Models.Briller brille4 = new Models.Briller("Chic Eyewear", 11.2, 12.8, "Hvid", 5.5, true);
            briller.Add(brille4);

            Models.Briller brille5 = new Models.Briller("Fashionable Goggles", 10.3, 13.2, "Sort", 4.2, false);
            briller.Add(brille5);

            Models.Briller brille6 = new Models.Briller("Trendy Shades", 10.8, 12.7, "Grøn", 5.1, true);
            briller.Add(brille6);

            Models.Briller brille7 = new Models.Briller("Hip Eyeglasses", 9.9, 11.9, "Hvid", 4.7, false);
            briller.Add(brille7);

            Models.Briller brille8 = new Models.Briller("Funky Frames", 11.5, 13.5, "Sort", 5.3, true);
            briller.Add(brille8);

            Models.Briller brille9 = new Models.Briller("Avant-Garde Glasses", 10.6, 12.3, "Hvid", 4.4, false);
            briller.Add(brille9);

            Models.Briller brille10 = new Models.Briller("Sleek Spectacles", 10.2, 13.1, "Sort", 4.9, true);
            briller.Add(brille10);

            Models.Briller brille11 = new Models.Briller("Urban Eyewear", 9.7, 11.3, "Grøn", 5.0, false);
            briller.Add(brille11);

            Models.Briller brille12 = new Models.Briller("Glamorous Goggles", 11.9, 12.6, "Hvid", 5.4, true);
            briller.Add(brille12);

            Models.Briller brille13 = new Models.Briller("Elegant Eyeglasses", 10.4, 13.3, "Sort", 4.6, false);
            briller.Add(brille13);

            Models.Briller brille14 = new Models.Briller("Edgy Frames", 10.7, 12.5, "Hvid", 5.7, true);
            briller.Add(brille14);

            Models.Briller brille15 = new Models.Briller("Vintage Shades", 9.8, 11.7, "Sort", 4.3, false);
            briller.Add(brille15);

            Models.Briller brille16 = new Models.Briller("Classic Eyewear", 9.6, 11.2, "Grøn", 4.1, true);
            briller.Add(brille16);

            Models.Briller brille17 = new Models.Briller("Bold Spectacles", 11.1, 13.4, "Hvid", 5.6, false);
            briller.Add(brille17);

            Models.Briller brille18 = new Models.Briller("Minimalist Frames", 10.9, 12.9, "Sort", 4.9, true);
            briller.Add(brille18);

            Models.Briller brille19 = new Models.Briller("Sleek Eyewear", 9.4, 11.8, "Hvid", 4.7, false);
            briller.Add(brille19);

            Models.Briller brille20 = new Models.Briller("Modern Goggles", 11.3, 13.0, "Sort", 4.4, true);
            briller.Add(brille20);

            Models.Briller brille21 = new Models.Briller("Elegant Shades", 10.5, 12.2, "Grøn", 5.3, false);
            briller.Add(brille21);

            Models.Briller brille22 = new Models.Briller("Unique Eyeglasses", 9.3, 11.6, "Hvid", 4.5, true);
            briller.Add(brille22);

            Models.Briller brille23 = new Models.Briller("Vintage Frames", 11.4, 13.3, "Sort", 5.2, false);
            briller.Add(brille23);

            Models.Briller brille24 = new Models.Briller("Trendy Eyewear", 10.7, 12.8, "Hvid", 4.8, true);
            briller.Add(brille24);

            Models.Briller brille25 = new Models.Briller("Chic Spectacles", 9.7, 11.5, "Sort", 4.6, false);
            briller.Add(brille25);

            Models.Briller brille26 = new Models.Briller("Fashion Frames", 10.3, 13.1, "Grøn", 5.5, true);
            briller.Add(brille26);

            Models.Briller brille27 = new Models.Briller("Hip Goggles", 9.9, 11.3, "Hvid", 4.2, false);
            briller.Add(brille27);

            Models.Briller brille28 = new Models.Briller("Funky Eyeglasses", 11.6, 13.5, "Sort", 5.1, true);
            briller.Add(brille28);

            Models.Briller brille29 = new Models.Briller("Avant-Garde Frames", 10.1, 12.4, "Hvid", 4.7, false);
            briller.Add(brille29);

            Models.Briller brille30 = new Models.Briller("Sleek Shades", 10.8, 12.7, "Sort", 5.0, true);
            briller.Add(brille30);

            Models.Briller brille31 = new Models.Briller("Urban Eyeglasses", 9.6, 11.9, "Grøn", 4.4, false);
            briller.Add(brille31);

            Models.Briller brille32 = new Models.Briller("Glamorous Goggles", 11.2, 12.6, "Hvid", 5.3, true);
            briller.Add(brille32);

            Models.Briller brille33 = new Models.Briller("Elegant Eyewear", 10.4, 13.3, "Sort", 4.6, false);
            briller.Add(brille33);

            Models.Briller brille34 = new Models.Briller("Edgy Frames", 10.7, 12.5, "Hvid", 5.7, true);
            briller.Add(brille34);

            Models.Briller brille35 = new Models.Briller("Vintage Shades", 9.8, 11.7, "Sort", 4.3, false);
            briller.Add(brille35);

            Models.Briller brille36 = new Models.Briller("Classic Spectacles", 10.9, 13.4, "Grøn", 5.6, true);
            briller.Add(brille36);

            Models.Briller brille37 = new Models.Briller("Bold Frames", 9.5, 11.2, "Hvid", 4.9, false);
            briller.Add(brille37);

            Models.Briller brille38 = new Models.Briller("Minimalist Eyewear", 11.3, 13.0, "Sort", 4.4, true);
            briller.Add(brille38);

            Models.Briller brille39 = new Models.Briller("Sleek Goggles", 10.4, 12.8, "Hvid", 4.7, false);
            briller.Add(brille39);

            Models.Briller brille40 = new Models.Briller("Modern Spectacles", 9.3, 11.6, "Sort", 4.5, true);
            briller.Add(brille40);

            Models.Briller brille41 = new Models.Briller("Elegant Frames", 10.6, 12.3, "Grøn", 5.2, false);
            briller.Add(brille41);

            Models.Briller brille42 = new Models.Briller("Unique Eyeglasses", 9.7, 11.5, "Hvid", 4.8, true);
            briller.Add(brille42);

            Models.Briller brille43 = new Models.Briller("Vintage Shades", 11.0, 13.1, "Sort", 5.5, false);
            briller.Add(brille43);

            Models.Briller brille44 = new Models.Briller("Trendy Eyewear", 10.5, 12.2, "Hvid", 4.6, true);
            briller.Add(brille44);

            Models.Briller brille45 = new Models.Briller("Chic Spectacles", 9.9, 11.3, "Sort", 5.3, false);
            briller.Add(brille45);

            Models.Briller brille46 = new Models.Briller("Fashion Frames", 10.7, 12.9, "Grøn", 4.4, true);
            briller.Add(brille46);

            Models.Briller brille47 = new Models.Briller("Hip Goggles", 9.8, 11.9, "Hvid", 5.7, false);
            briller.Add(brille47);

            Models.Briller brille48 = new Models.Briller("Funky Eyeglasses", 11.4, 13.5, "Sort", 4.2, true);
            briller.Add(brille48);

            Models.Briller brille49 = new Models.Briller("Avant-Garde Frames", 10.1, 12.4, "Hvid", 4.9, false);
            briller.Add(brille49);

            Models.Briller brille50 = new Models.Briller("Sleek Shades", 10.8, 12.7, "Sort", 4.7, true);
            briller.Add(brille50);

            return briller;
        }

    }
}
