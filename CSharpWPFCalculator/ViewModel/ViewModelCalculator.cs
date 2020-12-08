using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;


namespace CSharpWPFCalculator.ViewModel {
    class ViewModelCalculator : INotifyPropertyChanged  {
        public ViewModelCalculator()
        {
            NumberButtonCmd = new RelayCommand(new Action<object>(NumberButtonPressed));
            EqualButtonCmd = new RelayCommand(new Action<object>(EqualButtonPressed));
            SpecialOperatorButtonCmd = new RelayCommand(new Action<object>(SpecialOperatorButtonPressed));
        }

    

        private ICommand m_ButtonCommand1;
        private ICommand m_ButtonCommand2;
        private ICommand m_ButtonCommand3;
        private StringBuilder _SBCalculation = new StringBuilder();
        private string _Calculation;
       
        public ICommand NumberButtonCmd
        {
            get
            {
                return m_ButtonCommand1;
            }
            set
            {
                m_ButtonCommand1 = value;
            }
        }

        public ICommand EqualButtonCmd
        {
            get
            {
                return m_ButtonCommand2;
            }
            set
            {
                m_ButtonCommand2 = value;
            }
        }

        public ICommand SpecialOperatorButtonCmd {
            get { 
                return m_ButtonCommand3; 
            }
            set { 
                m_ButtonCommand3 = value;
            } 
        }

        public void NumberButtonPressed(object obj)
        {
            _SBCalculation.Append(obj.ToString());
            Calculation = _SBCalculation.ToString();
        }

        public void EqualButtonPressed(object obj)
        {
            doCalculate();
        }

        private void SpecialOperatorButtonPressed(object obj)
        {
            
            if (obj.ToString() == "SQRT") {
                Calculation = $"Sqrt({_SBCalculation.ToString()})";
                doCalculate();
            } else if( obj.ToString() == "PERC") {
                Calculation = $"{_SBCalculation.ToString()}/100";
                doCalculate(false);
            }

        }


        public void doCalculate(Boolean reset = true)
        {
            try
            {
                // use nCalc lib                
                var v = new NCalc.Expression(Calculation).Evaluate();
                Calculation = v.ToString();

                if (reset)
                {
                    _SBCalculation.Clear();
                    reset = false;
                } else
                {
                    _SBCalculation.Clear();
                    _SBCalculation.Append(Calculation);
                }

            }
            catch (Exception ex)
            {
                Calculation = "Error";

            }
           
        }
        #region properties

        public string Calculation {
            get { return _Calculation; }
            set { 
                _Calculation = value;
                OnPropertyChange("Calculation");
            }
        }

        #endregion



        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChange(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
