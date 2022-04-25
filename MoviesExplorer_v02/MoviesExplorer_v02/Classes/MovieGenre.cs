using System;
using System.Collections.Generic;
using System.Text;

using Xamarin.Forms;


namespace MoviesExplorer_v02.Classes
{

    public class MovieGenre
    {
        static private string[] sGenre = { "Obejrzane", "Anime", "Animacja", "Komiks", "Film", "Serial",
                                           "Akcja", "Dramat", "Fantasy", "Historyczny", "Horror", "Isekai", "Katastroficzny", "Komedia",
                                           "Kryminal", "Muzyczny", "Okruchy zycia", "Przygodowy", "Psychologiczny", "Romantyczny", "Sci-Fi",
                                           "Sportowy", "Tajemnica", "???" };

        //Constructors

        public MovieGenre()
        {

        }


        //Variables

        //


        //Methods

        public static void CreateCheckBoxes(System.EventHandler<Xamarin.Forms.CheckedChangedEventArgs> CheckedChanged, Xamarin.Forms.StackLayout GenreStackLeft, Xamarin.Forms.StackLayout GenreStackRight)  //Create CheckBoxes, CheckBox CheckedChange event, StackLayouts as Parents(on Left & Right side)
        {
            int i = 0;
            while (i < sGenre.Length)
            {
                CheckBox cB = new CheckBox { IsChecked = false };
                cB.CheckedChanged += CheckedChanged;
                StackLayout sL = new StackLayout
                {
                    Orientation = StackOrientation.Horizontal,
                    Children =
                    {
                        cB,
                        new Label
                        {
                            Text = sGenre[i],
                            VerticalOptions = LayoutOptions.Center
                        }
                    }
                };

                if (i <= sGenre.Length / 2) // Probable "<=" is better bc if there will be uneven amount of genree aditional one will go to the right side
                {
                    GenreStackLeft.Children.Add(sL);
                }
                else
                {
                    GenreStackRight.Children.Add(sL);
                }
                i++;
            }
        }

        public static void CreateCheckBoxes(System.EventHandler<Xamarin.Forms.CheckedChangedEventArgs> CheckedChanged, Xamarin.Forms.StackLayout GenreStack) //Create CheckBoxes, CheckBox CheckedChange event, StackLayout as Parent
        {
            int i = 0;
            while (i < sGenre.Length)
            {
                CheckBox cB = new CheckBox { IsChecked = false };
                cB.CheckedChanged += CheckedChanged;
                StackLayout sL = new StackLayout
                {
                    Orientation = StackOrientation.Horizontal,
                    Children =
                    {
                        cB,
                        new Label
                        {
                            Text = sGenre[i],
                            VerticalOptions = LayoutOptions.Center
                        }
                    }
                };

                GenreStack.Children.Add(sL);

                i++;
            }
        }

        public bool Contains()  // Probably TO DELETE ?
        {
            return false;
        }
    }
}
