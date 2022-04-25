using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

using SQLite;
using MoviesExplorer_v02.Classes;

/*
 
Version Notes:
    NuGet:
        NetStandard.Library v2.0.3
        sqlite-net-pcl v1.7.335
        Xamarin.Essentials v1.6.1
        Xamarin.Forms v5.0.0.2012
    Android SDK:
        Android 11.0-R (Android SDK Platform 30)
        Android 10.0-Q (Android SDK Platform 29)
        Android 9.0-Pie (Android SDK Platform 28)
        Android 8.1-Oreo (Android SDK Platform 27)
        Android SDK Command-line Tools v2.1
        Android SDK Platform Tools v30.0.4
        Android SDK Build Tool (v30.0.2, v29.0.3, v28.0.3, v27.0.3)
        Android Emulator v30.1.5
        Inne (Android SDK Tools v26.1.1, SDK Patch Applier v4 v1)
 
*/

namespace MoviesExplorer_v02
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();

            Classes.MovieGenre.CreateCheckBoxes(FilterChecked,FiltersSL);   // Can shorten name since I'm importing whole Classes folder
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            ShowMovieList(SearchB.Text.ToString());
        }

        private void ShowMovieList(string search)
        {

            if (true)
            {
                if (search != "")
                {
                    using (SQLiteConnection connection = new SQLiteConnection(App.FilePath))
                    {
                        string query = String.Format("SELECT * FROM MovieClass where MovieTitle LIKE '%{0}%' ORDER BY MovieId DESC", search.ToUpper());
                        List<MovieClass> movies = connection.Query<MovieClass>(query).ToList();
                        if (movies != null)
                        {
                            ListLabel.IsVisible = false;
                            MovieListView.ItemsSource = movies;
                            if(movies.Count == 0) { ListLabel.IsVisible = true; }
                        }
                        else
                        {
                            ListLabel.IsVisible = true;
                            MovieListView.ItemsSource = "";
                        }
                    }
                }
                else
                {
                    using (SQLiteConnection connection = new SQLiteConnection(App.FilePath))
                    {
                        connection.CreateTable<MovieClass>();
                        string query = String.Format("SELECT * FROM MovieClass ORDER BY MovieId DESC");
                        var movie = connection.Query<MovieClass>(query).ToList();

                        ListLabel.IsVisible = false;
                        MovieListView.ItemsSource = movie;
                    }
                }
            }
            else  // Copy of previous search
            {
                if (search != "")
                {
                    using (SQLiteConnection connection = new SQLiteConnection(App.FilePath))
                    {
                        List<MovieClass> movie = (from m in connection.Table<MovieClass>()
                                                  where m.MovieTitle.ToUpper().Contains(search.ToUpper())
                                                  select m).ToList();
                        if (movie != null) { MovieListView.ItemsSource = movie; }
                        else { MovieListView.ItemsSource = ""; }
                    }
                }
                else
                {
                    using (SQLiteConnection connection = new SQLiteConnection(App.FilePath))
                    {
                        connection.CreateTable<MovieClass>();
                        var movie = connection.Table<MovieClass>().ToList();

                        MovieListView.ItemsSource = movie;
                    }
                }
            }

            //add filters usedge and info when there is no movies to display
        }

        private void SearchB_TextChanged(object sender, TextChangedEventArgs e)
        {
            SearchBar searchBar = (SearchBar)sender;

            ShowMovieList(searchBar.Text.ToString());
        }

        private void AddBtn_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new EditPage());
        }

        private void MovieListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            //for what is that?
        }

        private void MovieListView_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            var movie = MovieListView.SelectedItem as MovieClass;
            Navigation.PushAsync(new EditPage(movie));
        }

        private void FiltersBtn_Clicked(object sender, EventArgs e)
        {
            if (FiltersViewer.IsVisible) { FiltersViewer.IsVisible = false; }
            else { FiltersViewer.IsVisible = true; }
        }

        private void FilterChecked(object sender, EventArgs e)
        {

        }

        private void TestCheckBox_CheckedChanged(object sender, CheckedChangedEventArgs e)
        {
            TempTest.CustomCheckBox cCB = (TempTest.CustomCheckBox)sender;
            cCB.OnClick();


        }

        private void TestCheckBox_ClickEvent(object sender, TempTest.StateChangedEventArgs e)
        {
            TempTest.CustomCheckBox cCB = (TempTest.CustomCheckBox)sender;
            //cCB.OnClick();
        }

        private void TestCheckBox_Clicked(object sender, EventArgs e)
        {
            TempTest.CustomCheckBox cCB = (TempTest.CustomCheckBox)sender;
            cCB.OnClick();
        }
    }
}


/*
TODO:

[ ] -add filters (add smt to compare with smt else from movie; add method to compare them; edit ShowMovieList method for showing good results) -> use SQL query commands
[ ] -add custom CheckBox for filters so they can go in modes like OR/AND/NOT
[ ] -add Button to check/uncheck all CheckBoxes
[ ] -add commants so I can know what this code actualy do ^.^'
[ ] -fix saving genre from EditPage -> use another connection between tables Movies and Genree
[^] -change Item Hight in ListView so all of them have same hight not depend of title and genree length
[ ] -fix MovieGenre class
[^] -add info display when there is no movie after search -> fix Lable
[ ] -add oder method -> selection list of ordering by id/title/genree | ASC/DESC e.g.("SELECT ... ORDER BY {0} {1}", selection.text, selection2.text) or ("SELECT ... ORDER BY {0} {1}", selection.text, boolIsASC.ToText)
[ ] -??? ?create new Xamarin Control (StackLayout(Horizontal) {CheckBox, Label}; 
                                  Properties: bool IsChecked {Bind to CheckBox}, eventArgs void CheckChanged {Binde to CheckBox}, string Text {Bind to Label} )
[ ] -

 */