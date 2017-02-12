using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventApp_WPF
{
    class Person
    {
        private State _state;
        public enum State
        {
            Born = 1,
            Died = 2,
            Married = 3,
            Divorced = 4,
            GotBaby = 5,
        }
        public enum GenderType
        {
            Male = 1,
            Female = 2,
        }
        public State MaritalState { get { return _state; } }
        public string Name { set; get; }
        public int Age { set; get; }
        public Person LifePair { set; get; }
        public List<Person> Children { set; get; }
        public GenderType Gender { set; get; }

        public event EventHandler Born;
        public event EventHandler Died;
        public event EventHandler Married;
        public event EventHandler Divorced;
        public event EventHandler GotBaby;

        public Person()
        {
            #region Pick Handlers
            this.Born += new EventHandler(OnBorn);
            this.Died += new EventHandler(OnDied);
            this.Married += new EventHandler(OnMarried);
            this.Divorced += new EventHandler(OnDivorced);
            this.GotBaby += new EventHandler(OnGotBaby);
            #endregion

            this.OnBorn(this, new EventArgs());
        }
        public Person(string Name, GenderType Gender)
            : this()
        {
            this.Name = Name;
            this.Gender = Gender;
        }
        public Person(string Name, int Age, GenderType Gender)
            : this()
        {
            this.Name = Name;
            this.Age = Age;
            this.Gender = Gender;
        }
        public void Kill()
        {
            this.OnDied(this, new EventArgs());
        }
        public void Marry(Person p)
        {
            if (this.Gender == p.Gender)
            {
                throw new InvalidGenderException();
            }
            this.OnMarried(this, new MarryEventArgs() { Wife = p });
        }
        public void Divorce()
        {
            this.LifePair = null;
            this.OnDivorced(this, new EventArgs());
        }
        public void AssignBaby(Person p)
        {
            if (this.Children == null) this.Children = new List<Person>();
            this.Children.Add(p);
            this.OnGotBaby(this, new EventArgs());
        }
        private void OnBorn(object sender, EventArgs e)
        {
            this._state = State.Born;
        }
        private void OnDied(object sender, EventArgs e)
        {
            this._state = State.Died;
        }
        private void OnMarried(object sender, EventArgs e)
        {
            this._state = State.Married;
        }
        private void OnDivorced(object sender, EventArgs e)
        {
            this._state = State.Divorced;
        }
        private void OnGotBaby(object sender, EventArgs e)
        {
            this._state = State.GotBaby;
        }
    }
    class InvalidGenderException : Exception
    {
        public override string Message
        {
            get
            {
                return "Two Genders are invalid, Must be a male and female.";
            }
        }
    }

    class MarryEventArgs : EventArgs
    {
        public Person Wife { set; get; }
    }
}
