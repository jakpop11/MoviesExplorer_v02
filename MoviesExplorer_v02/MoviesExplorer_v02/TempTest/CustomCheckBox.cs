using System;
using System.Collections.Generic;
using System.Text;

using Xamarin.Forms;

namespace MoviesExplorer_v02.TempTest
{
    public class CustomCheckBox: Button
    {

        public CustomCheckBox()
        {
            this.StateProperty = State.DEFAULT;
            SetUp();
        }

        public enum State
        {
            ANY,
            ALL,
            NONE,
            DEFAULT
        }

        public State StateProperty { get; set; }


        public event EventHandler<StateChangedEventArgs> ClickEvent;

        public void OnClick() 
        {
            this.ClickEvent += (sender, e) =>
            {
                Console.WriteLine($"ClickEvent occured. Sender: {sender}, Arg: {e.StateProperty.ToString()}");
            };

            StateChangedEventArgs args = new StateChangedEventArgs();
            Change();
            SetUp();
            args.StateProperty = this.StateProperty;
            ClickEvent.Invoke(this, args);
        }

        //private void SetUp()
        //{
        //    switch (this.StateProperty)
        //    {
        //        case State.ALL: 
        //            this.Color = Color.Blue;
        //            break;
        //        case State.ANY:
        //            this.Color = Color.Green;
        //            break;
        //        case State.NONE:
        //            this.Color = Color.Red;
        //            break;
        //        case State.DEFAULT:
        //            this.Color = Color.Gray;
        //            break;
        //        default:
        //            this.Color = Color.Yellow;
        //            break;
        //    }
        //}

        private void SetUp()
        {
            switch (this.StateProperty)
            {
                case State.ALL:
                    this.BackgroundColor = Color.Blue;
                    break;
                case State.ANY:
                    this.BackgroundColor = Color.Green;
                    break;
                case State.NONE:
                    this.BackgroundColor = Color.Red;
                    break;
                case State.DEFAULT:
                    this.BackgroundColor = Color.Gray;
                    break;
                default:
                    this.BackgroundColor = Color.Yellow;
                    break;
            }
        }

        private void Change()
        {
            switch (this.StateProperty)
            {
                case State.ALL:
                    this.StateProperty = State.ANY;
                    break;
                case State.ANY:
                    this.StateProperty = State.NONE;
                    break;
                case State.NONE:
                    this.StateProperty = State.DEFAULT;
                    break;
                case State.DEFAULT:
                    this.StateProperty = State.ALL;
                    break;
                default:
                    this.StateProperty = State.DEFAULT;
                    break;
            }
        }


    }

    public class StateChangedEventArgs : EventArgs
    {
        public CustomCheckBox.State StateProperty { get; set; }
    }



}
