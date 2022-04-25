using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using MoviesExplorer_v02.Classes;
using SQLite;


namespace MoviesExplorer_v02
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EditPage : ContentPage
    {
        public EditPage()
        {
            // Add movie
            InitializeComponent();
            OnStart();
        }

        public EditPage(MovieClass movie)
        {
            // Update selected movie
            InitializeComponent();

            TitleEntry.Text = movie.MovieTitle;
            DetailsEntry.Text = movie.MovieDetails;
            ImageBtn.Source = movie.MovieImageUrl;
            MoviePrimaryKey = movie.MovieId;
            MovieURL = movie.MovieImageUrl;
            OnStart(MovieGenre(movie.MovieGenre));
            //Alert("Movie Genre:", movie.MovieGenre, "OK");       // Is it working?
        }

        private int MoviePrimaryKey;
        private string MovieURL;

        public async void Alert(string title, string messange, string button)
        {
            await DisplayAlert(title, messange, button);
        }

        private string[] sGenre = { "Obejrzane", "Anime", "Animacja", "Komiks", "Film", "Serial",
                                    "Akcja", "Dramat", "Fantasy", "Historyczny", "Horror", "Isekai", "Katastroficzny", "Komedia",
                                    "Kryminal", "Muzyczny", "Okruchy zycia", "Przygodowy", "Psychologiczny", "Romantyczny", "Sci-Fi",
                                    "Sportowy", "Tajemnica", "???" };

        private void OnStart(bool b) //TO DELETE
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

                if (i <= sGenre.Length / 2)
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

        private void OnStart()
        {
            // Create check boxes
            Classes.MovieGenre.CreateCheckBoxes(CheckedChanged, GenreStackLeft, GenreStackRight);
            
        }

        private void OnStart(string[] sG)   // string which represents movie genree
        {
            // Create check boxes and turn correct ones on based on movie genree
            bool bChecked = false;
            int i = 0, j = 0;
            while (i < sGenre.Length)
            {
                if (sG[j] == sGenre[i]) 
                { 
                    bChecked = true;
                    if (sG.Length - 1 > j) { j++; }
                }
                else { bChecked = false; }

                CheckBox cB = new CheckBox { IsChecked = bChecked };
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

                if (i <= sGenre.Length / 2)
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

        private string CheckBoxCheck()
        {
            //StackLayout SL = (StackLayout)GenreStackLeft.Children.ElementAt(0);
            //GenreStackRight.Children.Add(SL);     // Move SL to new location

            string s = "";

            int countL = GenreStackLeft.Children.Count;
            for(int i=0; i<countL; i++)
            {
                StackLayout sL = (StackLayout)GenreStackLeft.Children.ElementAt(i);
                CheckBox cBox = (CheckBox)sL.Children.First();
                Label lB = (Label)sL.Children.Last();
                if (cBox.IsChecked)
                {
                    //Alert("Info", "CheckBox " + lB.Text + " " + i + "/" + tmp + " is checked.", "OK");
                    //DetailsEntry.Text += lB.Text + ", ";
                    s += lB.Text + "/";
                }
            }
            int countR = GenreStackRight.Children.Count;
            for(int i=0; i<countR; i++)
            {
                StackLayout sL = (StackLayout)GenreStackRight.Children.ElementAt(i);
                CheckBox cBox = (CheckBox)sL.Children.First();
                Label lB = (Label)sL.Children.Last();
                if (cBox.IsChecked)
                {
                    //DetailsEntry.Text += lB.Text + ", ";
                    s += lB.Text + "/";
                }
            }

            if (s.Length>1) { return s.Remove(s.Length - 1, 1); }
            else { return s; }       //s = "Obejrzane/Anime/Film/Akcja/Dramat/Fantasy/Katastroficzny/Komedia/Okruchy zycia/Romantyczny/Tajemnica/???"
        }

        private string[] MovieGenre(string s)   //s = "Obejrzane/Anime/Film/Akcja/Dramat/Fantasy/Katastroficzny/Komedia/Okruchy zycia/Romantyczny/Tajemnica/???"
        {
            string[] mGenre;
            if (s == null) { mGenre = new string[]{ " " }; }
            else
            {
                mGenre = s.Split('/');
                //for(int i=0; i<mGenre.Length; i++)  //mGenre.Length = 12
                //{
                //    DetailsEntry.Text += mGenre[i] + "{o.o}";
                //}
            }
                return mGenre;
        }

        private void SaveBtn_Clicked(object sender, EventArgs e)
        {
            MovieClass movie = new MovieClass()
            {
                MovieId = MoviePrimaryKey,
                MovieTitle = TitleEntry.Text.ToString(),
                MovieDetails = DetailsEntry.Text.ToString(),
                MovieGenre = CheckBoxCheck(),       // Is it working?
                MovieImageUrl = ImageBtn.Source.ToString().Remove(0, 5) // .Source.ToString() = "Uri: ..." -> .Source.ToString().Remove(0, 5)

                //Add MovieGenre Class?
            };

            using (SQLiteConnection connection = new SQLiteConnection(App.FilePath))
            {
                connection.CreateTable<MovieClass>();
                if (MoviePrimaryKey == 0)
                {
                    connection.Insert(movie);
                }
                else { connection.Update(movie); }
            }

            Navigation.PopAsync();
        }

        private void DeleteBtn_Clicked(object sender, EventArgs e)
        {
            if (MoviePrimaryKey != 0)
            {
                using (SQLiteConnection connection = new SQLiteConnection(App.FilePath))
                {
                    MovieClass movie = (from m in connection.Table<MovieClass>()
                                        where m.MovieId == MoviePrimaryKey
                                        select m).FirstOrDefault();
                    if (movie != null) { connection.Delete(movie); }
                }

                Navigation.PopAsync();
            }
            else { Navigation.PopAsync(); }

        }

        private async void MovieUrl()   // Display a window for passing URL
        {
            string result = await DisplayPromptAsync("Image URL:", "Enter image url", "Ok", "Cancel", initialValue: MovieURL);

            if(result != null)
            {
                if(result == "") { result = "https://images5.fanpop.com/image/photos/29000000/Death-the-Kid-mtndewluver-29044843-225-350.jpg"; }
                ImageBtn.Source = result;
            }
        }

        private void ImageBtn_Clicked(object sender, EventArgs e)
        {
            MovieUrl();
            //TitleEntry.Text = CheckBoxCheck();        // Testing
        }

        private void CheckedChanged(object sender, CheckedChangedEventArgs e)
        {
            //Alert("CheckBox", "CheckBox has been checked.", "oK");
        }
    }
}


/*
TODO:

[ ] -clear the code;
[ ] -change UI colors;
[ ] -fix checkboxes in columns so in both of them amount of checkboxes is even or higher on left side 
[ ] -

 */