using System;
using System.Collections.Generic;
using System.Text;

using SQLite;

namespace MoviesExplorer_v02.Classes
{
    public class MovieClass
    {
        //Constructors
        
        public MovieClass()
        {

        }


        //Variables

        [PrimaryKey, AutoIncrement]
        public int MovieId
        {
            get;
            set;
        }

        [NotNull]
        public string MovieTitle
        {
            get;
            set;
        }

        [NotNull]
        public string MovieImageUrl
        {
            get;
            set;
        }

        [NotNull]   // Probably should be NULL so there will be no need for leaving empty value (as: "")
        public string MovieDetails
        {
            get;
            set;
        }

        public string MovieGenre        // Is it working? ...At first sight after a while - I think so
        {
            get;
            set;
        }

        public MovieGenre movieGenre // Don't work in SQL need to use another table for this
        {
            get;
            set;
        }


        //Methods

        public override string ToString()
        {
            return "MovieClass";
        }

    }
}
