using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using ConsoleControllsLib;
using Newtonsoft.Json;

namespace Hospital
{
    public enum DayOfWeek
    {
        Monday,
        Tuesday,
        Wednesday,
        Thursday,
        Friday,
        Saturday,
        Sunday
    }

    [DataContract]
    public class Record
    {
        [DataMember] public string Name;
        [DataMember] public DateTime StartTime;
        [DataMember] public DateTime EndTime;
    }

    [DataContract]
    public class Doctor
    {
        public Doctor()
        {
            Monday = new List<Record>();
            Tuesday = new List<Record>();
            Wednesday = new List<Record>();
            Thursday = new List<Record>();
            Friday = new List<Record>();
            Saturday = new List<Record>();
            Sunday = new List<Record>();
        }

        [DataMember] public string Name { get; set; }
        [DataMember] public string Email { get; set; }
        [DataMember] public string Password { get; set; }

        public bool ValidateRevord(DayOfWeek day, Record record)
        {
            foreach (var r in GetRecordsOnDay(day))
            {
                if (record.StartTime > r.StartTime && record.StartTime < r.EndTime)
                    return false;

                if (record.EndTime > r.StartTime && record.EndTime < r.EndTime)
                    return false;
            }

            return true;
        }

        public List<Record> GetRecordsOnDay(DayOfWeek day) {


            List<Record> temp;

            switch (day)
            {
                case DayOfWeek.Monday:
                    return Monday;
                    break;
                case DayOfWeek.Tuesday:
                    return Tuesday;
                    break;
                case DayOfWeek.Wednesday:
                    return Wednesday;
                    break;
                case DayOfWeek.Thursday:
                    return Thursday;
                    break;
                case DayOfWeek.Friday:
                    return Friday;
                    break;
                case DayOfWeek.Saturday:
                    return Saturday;
                    break;
                case DayOfWeek.Sunday:
                    return Sunday;
                    break;
                default:
                    temp = new List<Record>();
                    break;
            }

            
            return temp;
        }

        public void AddRecordsOnDay(DayOfWeek day, Record record)
        {
            var temp = new List<Record>();

            switch (day)
            {
                case DayOfWeek.Monday:
                    Monday.Add(record);
                    Monday.Sort((a, b) => a.StartTime.CompareTo(b.StartTime));
                    break;
                case DayOfWeek.Tuesday:
                    Tuesday.Add(record);

                    Tuesday.Sort((a, b) => a.StartTime.CompareTo(b.StartTime));
                    break;
                case DayOfWeek.Wednesday:
                     Wednesday.Add(record);

                    Wednesday.Sort((a, b) => a.StartTime.CompareTo(b.StartTime));
                    break;
                case DayOfWeek.Thursday:
                    Thursday.Add(record);

                    Thursday.Sort((a, b) => a.StartTime.CompareTo(b.StartTime));
                    break;
                case DayOfWeek.Friday:
                    Friday.Add(record);

                    Friday.Sort((a, b) => a.StartTime.CompareTo(b.StartTime));
                    break;
                case DayOfWeek.Saturday:
                    Saturday.Add(record);

                    Saturday.Sort((a, b) => a.StartTime.CompareTo(b.StartTime));
                    break;
                case DayOfWeek.Sunday:
                    Sunday.Add(record);

                    Sunday.Sort((a, b) => a.StartTime.CompareTo(b.StartTime));
                    break;
                default:
                    break;
            }


        }
        private List<Record>[] Records = new List<Record>[7];


        [DataMember] private List<Record> Monday ;
        [DataMember] private List<Record> Tuesday ;
        [DataMember] private List<Record> Wednesday ;
        [DataMember] private List<Record> Thursday;
        [DataMember] private List<Record> Friday ;
        [DataMember] private List<Record> Saturday;
        [DataMember] private List<Record> Sunday;
    }


    public class App
    {
        string DoctorsFilePath = @"Doctor.json";

        public App()
        {
            

            if (File.Exists(DoctorsFilePath))
            {
                Doctors = JsonConvert.DeserializeObject<List<Doctor>>(File.ReadAllText(DoctorsFilePath)) ?? new List<Doctor>();

            }
            else
            {
                Doctors = new List<Doctor>();
                File.Create(DoctorsFilePath);
            }

            #region RegScreen


            var RegFullNameInput = new InputField()
            {
                Position = (0.5, 0.1),
                Size = (0.8, 0.4),
            };

            var RegEMailInput = new InputField()
            {
                Position = (0.5, 0.1),
                Size = (0.8, 0.4),
            };

            var RegPasswordInput = new PasswordBox()
            {
                Position = (0.5, 0.1),
                Size = (0.8, 0.4),
            };

            var RegPasswordConfirmInput = new PasswordBox()
            {
                Position = (0.5, 0.1),
                Size = (0.8, 0.4),
            };

            var RegInformTextBox = new TextBox()
            {
                Position = (0.7, 0.1),
                Size = (0.8, 0.14)
            };

            var RegConfirmButton = new Button()
            {
                Position = (0.2, 0.1),
                Size = (0.8, 0.5),
                Text = "Confirm",

                OnClick = (sender, args) =>
                {

                    if (RegPasswordConfirmInput.PassString != RegPasswordInput.PassString)
                    {
                        RegInformTextBox.Text = "Passwors must match";
                        return;
                    }

                    if (RegFullNameInput.Text == "")
                    {
                        RegInformTextBox.Text = "Input valid names";
                        return;
                    }

                    Doctor doctor = new Doctor()
                    {
                        Name = RegFullNameInput.Text,
                        Email = RegEMailInput.Text,
                        Password = RegPasswordInput.PassString
                    };

                    Doctors.Add(doctor);

                    CurrDoctor = doctor;

                    SaveFile();

                    Window._ActiveScreen = HellowScreen;

                }
            };

            RegScreen.AddChild(

                new Table()
                {
                    Position = (0, 0),
                    Size = (1, 1),

                    Childrens =
                    {
                        new Table()
                        {
                            Position = (0.05, 0.1),
                            Size = (0.8, 0.15),

                            Childrens =
                            {
                                new TextBox()
                                {
                                    Position = (0, 0.1),
                                    Size = (0.8, 0.4),
                                    Text = "Full Name"
                                },

                                RegFullNameInput
                            }
                        },

                        new Table()
                        {
                            Position = (0.2, 0.1),
                            Size = (0.8, 0.15),

                            Childrens =
                            {
                                new TextBox()
                                {
                                    Position = (0, 0.1),
                                    Size = (0.8, 0.4),
                                    Text = "Email"
                                },

                                RegEMailInput
                            }
                        },

                        new Table()
                        {
                            Position = (0.38, 0.1),
                            Size = (0.8, 0.15),

                            Childrens =
                            {
                                new TextBox()
                                {
                                    Position = (0, 0.1),
                                    Size = (0.8, 0.4),
                                    Text = "Password"
                                },

                                RegPasswordInput
                            }
                        },

                        new Table()
                        {
                            Position = (0.55, 0.1),
                            Size = (0.8, 0.15),

                            Childrens =
                            {
                                new TextBox()
                                {
                                    Position = (0, 0.1),
                                    Size = (0.8, 0.4),
                                    Text = "Confirm Password"
                                },

                                RegPasswordConfirmInput
                            }
                        },

                        RegInformTextBox,

                        new Table()
                        {
                            Position = (0.85, 0.1),
                            Size = (0.8, 0.1),

                            Childrens =
                            {
                                RegConfirmButton
                            }
                        }
                    }

                });

            RegScreen.Init();

            #endregion

            #region HelloScreen



            HellowScreen.AddChild(
                new Table()
                {
                    Position = (0.1, 0.1),
                    Size = (0.8, 0.8),

                    Childrens = {

                        new TextBox()
                        {
                            Position = (0.1 , 0.1),
                            Size = (0.8 , 0.5),

                            Text = "Welcome"
                        },

                        new Table()
                        {
                            Position = (0.7 , 0.1),
                            Size = (0.8 , 0.2),

                            Childrens =
                            {

                                new Button()
                                {
                                    Position = (0.25 , 0.1),
                                    Size = (0.4 , 0.5),

                                    Text  = "Quit",

                                    OnClick = (sender, args) =>
                                    {
                                        Window._ActiveScreen = AuthScreen;
                                        this.ResetCurrentDoctor();
                                    }
                                },

                                new Button()
                                {
                                    Position = (0.25 , 0.5),
                                    Size = (0.4 , 0.5),

                                    Text  = "View Table",

                                    OnClick = (sender, args) =>
                                    {
                                        Window._ActiveScreen = this.CreateWeekTableScreen();
                                    }
                                }

                            }

                        }

                    }

                }
                );

            HellowScreen.Init();

            #endregion

            #region WeekTableScreen



            var MondayPatientTextBox = new TextBox()
            {
                Position = (0, 0.5),
                Size = (0.4, 0.7),
                Text = "Patient: 1"
            };

            var TuesdayPatientTextBox = new TextBox()
            {
                Position = (0, 0.5),
                Size = (0.4, 0.7),
                Text = "Patient: 1"
            };

            var WednesdayPatientTextBox = new TextBox()
            {
                Position = (0, 0.5),
                Size = (0.4, 0.7),
                Text = "Patient: 1"
            };

            var ThursdayPatientTextBox = new TextBox()
            {
                Position = (0, 0.5),
                Size = (0.4, 0.7),
                Text = "Patient: 1"
            };

            var FridayPatientTextBox = new TextBox()
            {
                Position = (0, 0.5),
                Size = (0.4, 0.7),
                Text = "Patient: 1"
            };

            var SaturdaysPatientTextBox = new TextBox()
            {
                Position = (0, 0.5),
                Size = (0.4, 0.7),
                Text = "Patient: 1"
            };

            var SundayPatientTextBox = new TextBox()
            {
                Position = (0, 0.5),
                Size = (0.4, 0.7),
                Text = "Patient: 1"
            };

            WeekTableScreen.AddChild(

                new Table()
                {
                    Position = (0.1, 0.1),
                    Size = (0.8, 0.8),

                    Childrens =
                    {
                        new TextBox()
                        {
                            Position = (0 , 0.1),
                            Size = (0.8 , 0.1),
                            Text = "Week Table"
                        },

                        new Table()
                        {
                            Position = (0.2 , 0.1),
                            Size = (0.8 , 0.1),

                            Childrens =
                            {
                                new Button()
                                {
                                    Position = (0 , 0.1),
                                    Size =  (0.4, 0.7),
                                    Text = "Monday"
                                },
                                MondayPatientTextBox
                            }
                        },

                        new Table()
                        {
                            Position = (0.3 , 0.1),
                            Size = (0.8 , 0.1),
                             Childrens =
                            {
                                new Button()
                                {
                                    Position = (0 , 0.1),
                                    Size =  (0.4, 0.7),
                                    Text = "Tuesday"
                                },
                                TuesdayPatientTextBox
                            }
                        },

                        new Table()
                        {
                            Position = (0.4 , 0.1),
                            Size = (0.8 , 0.1),
                             Childrens =
                            {
                                new Button()
                                {
                                    Position = (0 , 0.1),
                                    Size =  (0.4, 0.7),
                                    Text = "Wednesday"
                                },
                                WednesdayPatientTextBox
                            }
                        },

                        new Table()
                        {
                            Position = (0.5 , 0.1),
                            Size = (0.8 , 0.1),
                             Childrens =
                            {
                                new Button()
                                {
                                    Position = (0 , 0.1),
                                    Size =  (0.4, 0.7),
                                    Text = "Thursday"
                                },
                                ThursdayPatientTextBox
                            }
                        },

                        new Table()
                        {
                            Position = (0.6 , 0.1),
                            Size = (0.8 , 0.1),
                             Childrens =
                            {
                                new Button()
                                {
                                    Position = (0 , 0.1),
                                    Size =  (0.4, 0.7),
                                    Text = "Friday"
                                },

                                FridayPatientTextBox
                            }
                        },

                        new Table()
                        {
                            Position = (0.7 , 0.1),
                            Size = (0.8 , 0.1),
                             Childrens =
                            {
                                new Button()
                                {
                                    Position = (0 , 0.1),
                                    Size =  (0.4, 0.7),
                                    Text = "Saturdays"
                                },
                                SaturdaysPatientTextBox
                            }
                        },

                        new Table()
                        {
                            Position = (0.8 , 0.1),
                            Size = (0.8 , 0.1),
                             Childrens =
                            {
                                new Button()
                                {
                                    Position = (0 , 0.1),
                                    Size =  (0.4, 0.7),
                                    Text = "Sunday"
                                },
                               SundayPatientTextBox
                            }
                        }
                    }

                }

            );


            WeekTableScreen.Init();

            #endregion

            #region DalilyTableScreen



            #endregion

            #region PatientCreateScreen




            #endregion

            #region AuthScreen



            var PasswordInput = new PasswordBox()
            {
                Position = (0.5, 0.1),
                Size = (0.8, 0.3)
            };

            var EMailInput = new InputField()
            {
                Position = (0.5, 0.1),
                Size = (0.8, 0.3)
            };

            var InfoTextBox = new TextBox()
            {
                Position = (0.8, 0.1),
                Size = (0.8, 0.1),
            };

            var LogInButton = new Button()
            {
                Position = (0.2, 0.1),
                Size = (0.3, 0.5),
                Text = "Log-In",

                OnClick = (sender, args) =>
                {
                    var response = this.TryToAuth(EMailInput.Text, PasswordInput.PassString);

                    if (response.res)
                    {
                        Window._ActiveScreen = HellowScreen;
                        return;
                    }

                    InfoTextBox.Text = response.msg;

                }
            };

            var SingInButton = new Button()
            {

                Position = (0.2, 0.6),
                Size = (0.3, 0.5),
                Text = "Sing-In",

                OnClick = (sender, args) =>
                {

                    Window._ActiveScreen = RegScreen;

                }
            };

            AuthScreen.AddChild(
                new Table()
                {
                    Position = (0.1, 0.1),
                    Size = (0.8, 0.8),

                    Childrens = {

                        new Table()
                        {
                            Position = (0.1, 0.1),
                            Size = (0.8, 0.25),
                            Childrens =
                            {
                                new TextBox()
                                {
                                    Position = (0.1, 0.1),
                                    Size = (0.8, 0.3),
                                    Text = "E-mail"
                                },

                                EMailInput
                            }
                        },

                        new Table()
                        {
                            Position = (0.4, 0.1),
                            Size = (0.8, 0.25),
                             Childrens =
                            {
                                new TextBox()
                                {
                                    Position = (0.1, 0.1),
                                    Size = (0.8, 0.3),
                                    Text = "Password"
                                },

                                PasswordInput
                            }
                        },

                        new Table()
                        {
                            Position = (0.7, 0.1),
                            Size = (0.8, 0.1),

                            Childrens =
                            {
                                LogInButton ,
                                SingInButton
                            }
                        },

                        InfoTextBox

                    }
                }
            );


            AuthScreen.Init();

            #endregion

        }

        private void SaveFile()
        {
            File.WriteAllText(DoctorsFilePath, JsonConvert.SerializeObject(Doctors ));
        }

        Screen RegScreen = new Screen(Window.instanse);
        Screen HellowScreen = new Screen(Window.instanse);
        Screen WeekTableScreen = new Screen(Window.instanse);

        public Screen AuthScreen = new Screen(Window.instanse);

        public (bool res, string msg) TryToAuth(string email, string pass)
        {
            //if (!ValidateEmail(email))
            //    return (false, "Invalid email");


            var doc = Doctors.FirstOrDefault(i => (i.Email == email && i.Password == pass));

            if (doc == null)
                return (false, "Invalid email or password");

            CurrDoctor = doc;
            return (true, "");

        }

        public void ResetCurrentDoctor() => CurrDoctor = null;

        public Screen CreatePatientAddScreebn(DayOfWeek day)
        {
            Screen PatientCreateScreen = new Screen(Window.instanse);

            TextBox InfoBox = new TextBox()
            {
                Position = (0.7, 0.1),

                Size = (0.8, 0.2)
            };

            InputField PatientNameInput = new InputField()
            {
                Size = (0.8, 0.5),
                Position = (0.4, 0.1)
            };

            InputField StartTimeInput = new InputField()
            {
                Size = (0.8, 0.5),
                Position = (0.4, 0.1)
            };

            InputField EndTimeInput = new InputField()
            {
                Size = (0.8, 0.5),
                Position = (0.4, 0.1)
            };


            PatientCreateScreen.AddChild(
                    new Table()
                    {
                        Size = (0.8, 0.8),
                        Position = (0.1, 0.1),

                        Childrens =
                        {

                            new TextBox()
                            {
                                Position = (0 , 0.1),
                                Size = (0.8 , 0.1),
                                Text = "Patien Screen"
                            },

                            new Table()
                            {

                                Position = (0.1 ,0.1),

                                Size = (0.8 , 0.2),

                                Childrens =
                                {

                                    new TextBox()
                                    {
                                        Size = (0.8, 0.4),
                                        Position = (0, 0.1),
                                        Text = "Patient Full Name"
                                    },

                                    PatientNameInput


                                }
                            },

                            new Table()
                            {

                                Position = (0.3 ,0.1),

                                Size = (0.8 , 0.2),

                                Childrens =
                                {

                                    new TextBox()
                                    {
                                        Size = (0.8, 0.4),
                                        Position = (0, 0.1),
                                        Text = "Start Time (Format hh mm)"
                                    },


                                    StartTimeInput

                                }
                            },

                            new Table()
                            {

                                Position = (0.5 ,0.1),

                                Size = (0.8 , 0.2),

                                Childrens =
                                {

                                    new TextBox()
                                    {
                                        Size = (0.8, 0.4),
                                        Position = (0, 0.1),
                                        Text = "End Time (Format hh mm)"
                                    },

                                   EndTimeInput

                                }
                            },

                            InfoBox,

                            new Table()
                            {
                                Position = (0.9 ,0.1),

                                Size = (0.8 , 0.1),

                                Childrens = {

                                    new Button()
                                    {
                                        Position = (0 ,0.2),
                                        Size = (0.3 , 1),
                                        Text = "Back",
                                        OnClick = (sender, args) =>
                                        {
                                            Window._ActiveScreen = CreateWeekTableScreen();
                                        }
                                    },
                                    new Button()
                                    {
                                        Position = (0 ,0.5),
                                        Size = (0.3 , 1),
                                        Text = "Add",

                                        OnClick = (sender , args) =>
                                        {

                                            try{

                                                var startText  = StartTimeInput.Text;
                                                var endtText  = EndTimeInput.Text;



                                                var startTime = Convert.ToDateTime(startText.Replace(' ' , ':'));
                                                var endTime = Convert.ToDateTime(endtText.Replace(' ', ':'));
                                                var name = PatientNameInput.Text;

                                                if (startTime > endTime)
                                                    throw new Exception();

                                                Record record = new Record()
                                                {
                                                    Name = name,
                                                    StartTime = startTime,
                                                    EndTime = endTime
                                                };

                                                if (!CurrDoctor.ValidateRevord(day , record))
                                                {
                                                    InfoBox.Text = "Time overlaps with another patient";
                                                    return;
                                                }

                                                CurrDoctor.AddRecordsOnDay(day, record);

                                                Window._ActiveScreen  = CreateWeekTableScreen();

                                            }
                                            catch (Exception ex)
                                            {
                                                InfoBox.Text = "Input invalid data";
                                            }
                                        }
                                    }
                                }
                            }

                    }
                    });



            PatientCreateScreen.Init();

            return PatientCreateScreen;
        }

        public Screen CreatePatientEditScreebn(DayOfWeek day, Record record)
        {
            Screen PatientCreateScreen = new Screen(Window.instanse);

            TextBox InfoBox = new TextBox()
            {
                Position = (0.7, 0.1),

                Size = (0.8, 0.2)
            };

            InputField PatientNameInput = new InputField()
            {
                Size = (0.8, 0.5),
                Position = (0.4, 0.1),
                Text = record.Name
            };

            InputField StartTimeInput = new InputField()
            {
                Size = (0.8, 0.5),
                Position = (0.4, 0.1),
                Text = record.StartTime.ToShortTimeString()
            };

            InputField EndTimeInput = new InputField()
            {
                Size = (0.8, 0.5),
                Position = (0.4, 0.1),
                Text = record.EndTime.ToShortTimeString()
            };


            PatientCreateScreen.AddChild(
                    new Table()
                    {
                        Size = (0.8, 0.8),
                        Position = (0.1, 0.1),

                        Childrens =
                        {

                            new TextBox()
                            {
                                Position = (0 , 0.1),
                                Size = (0.8 , 0.1),
                                Text = "Patien Screen"
                            },

                            new Table()
                            {

                                Position = (0.1 ,0.1),

                                Size = (0.8 , 0.2),

                                Childrens =
                                {

                                    new TextBox()
                                    {
                                        Size = (0.8, 0.4),
                                        Position = (0, 0.1),
                                        Text = "Patient Full Name"
                                    },

                                    PatientNameInput


                                }
                            },

                            new Table()
                            {

                                Position = (0.3 ,0.1),

                                Size = (0.8 , 0.2),

                                Childrens =
                                {

                                    new TextBox()
                                    {
                                        Size = (0.8, 0.4),
                                        Position = (0, 0.1),
                                        Text = "Start Time (Format hh mm)"
                                    },


                                    StartTimeInput

                                }
                            },

                            new Table()
                            {

                                Position = (0.5 ,0.1),

                                Size = (0.8 , 0.2),

                                Childrens =
                                {

                                    new TextBox()
                                    {
                                        Size = (0.8, 0.4),
                                        Position = (0, 0.1),
                                        Text = "End Time (Format hh mm)"
                                    },

                                   EndTimeInput

                                }
                            },

                            InfoBox,

                            new Table()
                            {
                                Position = (0.9 ,0.1),

                                Size = (0.8 , 0.1),

                                Childrens = {

                                    new Button()
                                    {
                                        Position = (0 ,0.2),
                                        Size = (0.3 , 1),
                                        Text = "Back",
                                        OnClick = (sender, args) =>
                                        {
                                            Window._ActiveScreen = CreateWeekTableScreen();
                                        }
                                    },
                                    new Button()
                                    {
                                        Position = (0 ,0.5),
                                        Size = (0.3 , 1),
                                        Text = "Edit",

                                        OnClick = (sender , args) =>
                                        {

                                            try{

                                                var startText  = StartTimeInput.Text;
                                                var endtText  = EndTimeInput.Text;



                                                var startTime = Convert.ToDateTime(startText.Replace(' ' , ':'));
                                                var endTime = Convert.ToDateTime(endtText.Replace(' ', ':'));
                                                var name = PatientNameInput.Text;

                                                if (startTime > endTime)
                                                    throw new Exception();

                                                Record _record = new Record()
                                                {
                                                    Name = name,
                                                    StartTime = startTime,
                                                    EndTime = endTime
                                                };

                                                if (!CurrDoctor.ValidateRevord(day , record))
                                                {
                                                    InfoBox.Text = "Time overlaps with another patient";
                                                    return;
                                                }

                                                int index = CurrDoctor.GetRecordsOnDay(day).IndexOf(record);

                                                CurrDoctor.GetRecordsOnDay(day)[index] = _record;

                                                Window._ActiveScreen  = CreateWeekTableScreen();

                                            }
                                            catch (Exception ex)
                                            {
                                                InfoBox.Text = "Input invalid data";
                                            }
                                        }
                                    }
                                }
                            }

                    }
                    });



            PatientCreateScreen.Init();

            return PatientCreateScreen;
        }

        public Screen CreateWeekTableScreen()
        {

            Screen WeekTableScreen = new Screen(Window.instanse);

            var MondayPatientTextBox = new TextBox()
            {
                Position = (0, 0.5),
                Size = (0.4, 0.7),
                Text = $"Patient: {CurrDoctor.GetRecordsOnDay(DayOfWeek.Monday).Count()}"
            };

            var TuesdayPatientTextBox = new TextBox()
            {
                Position = (0, 0.5),
                Size = (0.4, 0.7),
                Text = $"Patient: {CurrDoctor.GetRecordsOnDay(DayOfWeek.Tuesday).Count()}"
            };

            var WednesdayPatientTextBox = new TextBox()
            {
                Position = (0, 0.5),
                Size = (0.4, 0.7),
                Text = $"Patient: {CurrDoctor.GetRecordsOnDay(DayOfWeek.Wednesday).Count()}"
            };

            var ThursdayPatientTextBox = new TextBox()
            {
                Position = (0, 0.5),
                Size = (0.4, 0.7),
                Text = $"Patient: {CurrDoctor.GetRecordsOnDay(DayOfWeek.Thursday).Count()}"
            };

            var FridayPatientTextBox = new TextBox()
            {
                Position = (0, 0.5),
                Size = (0.4, 0.7),
                Text = $"Patient: {CurrDoctor.GetRecordsOnDay(DayOfWeek.Friday).Count()}"
            };

            var SaturdaysPatientTextBox = new TextBox()
            {
                Position = (0, 0.5),
                Size = (0.4, 0.7),
                Text = $"Patient: {CurrDoctor.GetRecordsOnDay(DayOfWeek.Saturday).Count()}"
            };

            var SundayPatientTextBox = new TextBox()
            {
                Position = (0, 0.5),
                Size = (0.4, 0.7),
                Text = $"Patient: {CurrDoctor.GetRecordsOnDay(DayOfWeek.Sunday).Count()}"
            };

            WeekTableScreen.AddChild(

                new Table()
                {
                    Position = (0.1, 0.1),
                    Size = (0.8, 0.8),

                    Childrens =
                    {
                        new TextBox()
                        {
                            Position = (0 , 0.1),
                            Size = (0.8 , 0.1),
                            Text = "Week Table"
                        },

                        new Table()
                        {
                            Position = (0.2 , 0.1),
                            Size = (0.8 , 0.1),

                            Childrens =
                            {
                                new Button()
                                {
                                    Position = (0 , 0.1),
                                    Size =  (0.4, 0.7),
                                    Text = "Monday",
                                    OnClick = (sender, args) =>
                                    {
                                        Window._ActiveScreen = CreateDailyTableScreen(CurrDoctor.GetRecordsOnDay(DayOfWeek.Monday), "Monday" , DayOfWeek.Monday );
                                    }
                                },
                                MondayPatientTextBox
                            }
                        },

                        new Table()
                        {
                            Position = (0.3 , 0.1),
                            Size = (0.8 , 0.1),
                             Childrens =
                            {
                                new Button()
                                {
                                    Position = (0 , 0.1),
                                    Size =  (0.4, 0.7),
                                    Text = "Tuesday",
                                    OnClick = (sender, args) =>
                                    {
                                        Window._ActiveScreen = CreateDailyTableScreen(CurrDoctor.GetRecordsOnDay(DayOfWeek.Tuesday), "Tuesday" , DayOfWeek.Tuesday );
                                    }
                                },
                                TuesdayPatientTextBox
                            }
                        },

                        new Table()
                        {
                            Position = (0.4 , 0.1),
                            Size = (0.8 , 0.1),
                             Childrens =
                            {
                                new Button()
                                {
                                    Position = (0 , 0.1),
                                    Size =  (0.4, 0.7),
                                    Text = "Wednesday",
                                    OnClick = (sender, args) =>
                                    {
                                        Window._ActiveScreen = CreateDailyTableScreen(CurrDoctor.GetRecordsOnDay(DayOfWeek.Wednesday), "Wednesday", DayOfWeek.Wednesday );
                                    }
                                },
                                WednesdayPatientTextBox
                            }
                        },

                        new Table()
                        {
                            Position = (0.5 , 0.1),
                            Size = (0.8 , 0.1),
                             Childrens =
                            {
                                new Button()
                                {
                                    Position = (0 , 0.1),
                                    Size =  (0.4, 0.7),
                                    Text = "Thursday",
                                    OnClick = (sender, args) =>
                                    {
                                        Window._ActiveScreen = CreateDailyTableScreen(CurrDoctor.GetRecordsOnDay(DayOfWeek.Thursday), "Thursday", DayOfWeek.Thursday );
                                    }
                                },
                                ThursdayPatientTextBox
                            }
                        },

                        new Table()
                        {
                            Position = (0.6 , 0.1),
                            Size = (0.8 , 0.1),
                             Childrens =
                            {
                                new Button()
                                {
                                    Position = (0 , 0.1),
                                    Size =  (0.4, 0.7),
                                    Text = "Friday",
                                    OnClick = (sender, args) =>
                                    {
                                        Window._ActiveScreen = CreateDailyTableScreen(CurrDoctor.GetRecordsOnDay(DayOfWeek.Friday), "Friday", DayOfWeek.Friday );
                                    }
                                },

                                FridayPatientTextBox
                            }
                        },

                        new Table()
                        {
                            Position = (0.7 , 0.1),
                            Size = (0.8 , 0.1),
                             Childrens =
                            {
                                new Button()
                                {
                                    Position = (0 , 0.1),
                                    Size =  (0.4, 0.7),
                                    Text = "Saturdays",
                                    OnClick = (sender, args) =>
                                    {
                                        Window._ActiveScreen = CreateDailyTableScreen(CurrDoctor.GetRecordsOnDay(DayOfWeek.Saturday), "Saturdays" , DayOfWeek.Saturday);
                                    }
                                },
                                SaturdaysPatientTextBox
                            }
                        },

                        new Table()
                        {
                            Position = (0.8 , 0.1),
                            Size = (0.8 , 0.1),
                             Childrens =
                            {
                                new Button()
                                {
                                    Position = (0 , 0.1),
                                    Size =  (0.4, 0.7),
                                    Text = "Sunday",
                                    OnClick = (sender, args) =>
                                    {
                                        Window._ActiveScreen = CreateDailyTableScreen(CurrDoctor.GetRecordsOnDay(DayOfWeek.Sunday), "Sunday" , DayOfWeek.Sunday );
                                    }
                                },
                                SundayPatientTextBox
                            }
                        },

                        new Table()
                        {
                            Position = (0.9 , 0.1),
                            Size = (0.8 , 0.1),

                            Childrens =
                            {
                                new Button()
                                {
                                    Position = (0 , 0.3),
                                    Size = (0.5, 1),
                                    Text = "Back",

                                    OnClick = (sender , args) =>{
                                        Window._ActiveScreen = this.HellowScreen;
                                    }
                                }
                            }

                        }
                    }

                }

            );

            WeekTableScreen.Init();

            return WeekTableScreen;
        }

        public Screen CreateDailyTableScreen(List<Record> records, string day, DayOfWeek _day)
        {
            Screen DailyTableScreen = new Screen(Window.instanse);

            Table table = new Table()
            {
                Position = (0.1, 0.1),
                Size = (0.8, 0.8),

                Childrens =
                {
                    new TextBox()
                        {
                            Position = (0 , 0.1),
                            Size = (0.8 , 0.1),
                            Text = day
                        }
                }
            };

            if (records.Count == 0)
                table.AddChild(
                new Table()
                {
                    Position = (0.2, 0.1),
                    Size = (0.8, 0.1),

                    Childrens =
                    {
                        new TextBox()
                        {
                            Position = (0.2, 0.1),
                            Size = (0.8, 0.8),
                            Text = "No records for this day"
                        }
                    }

                }
            );

            for (int i = 0; i < records.Count && i < 5; i++)
            {

                table.AddChild(
                new Table()
                {
                    Position = (0.1 * (i + 1), 0.1),
                    Size = (0.8, 0.1),

                    Childrens =
                    {
                            new TextBox()
                            {
                                Position = (0, 0),
                                Size = (0.3, 1),
                                Text = records[i].Name
                            },

                            new TextBox()
                            {
                                Position = (0, 0.3),
                                Size = (0.25,  1),
                                Text = $"{records[i].StartTime.ToShortTimeString()} - {records[i].EndTime.ToShortTimeString()}"
                            },

                            new Button()
                            {
                                Position = (0, 0.6),
                                Size = (0.15,  1),
                                Text = "Edit",
                                Data = i,

                                OnClick = (sender, args ) =>
                                {
                                    Window._ActiveScreen = CreatePatientEditScreebn( _day ,  CurrDoctor.GetRecordsOnDay(_day).ElementAt((int)(sender as Button).Data));
                                }
                            },

                            new Button()
                            {
                                Position = (0, 0.75),
                                Size = (0.15,  1),
                                Text = "Del",
                                Data = i,

                                OnClick = (sender, args ) =>
                                {
                                    CurrDoctor.GetRecordsOnDay(_day).RemoveAt((int)(sender as Button).Data);

                                    Window._ActiveScreen = CreateDailyTableScreen(  CurrDoctor.GetRecordsOnDay(_day), day , _day);
                                }


                            }

                    }
                });
            }

            table.AddChild(
                new Table()
                {
                    Position = (0.9, 0.1),

                    Size = (0.8, 0.1),

                    Childrens = {

                        new Button()
                        {
                            Position = (0 ,0.2),
                            Size = (0.3 , 1),
                            Text = "Back",

                            OnClick = (sender, args) => {
                                Window._ActiveScreen = CreateWeekTableScreen();
                            }


                        },
                        new Button()
                        {
                            Position = (0 ,0.5),
                            Size = (0.3 , 1),
                            Text = "Add",

                            OnClick = (sender,args) =>
                            {
                                Window._ActiveScreen = CreatePatientAddScreebn(_day);
                            }
                        }
                    }
                });

            DailyTableScreen.AddChild(
                table
            );

            DailyTableScreen.Init();

            SaveFile();

            return DailyTableScreen;
        }




        private bool ValidateEmail(string email)
        {
            string pattern = "[.\\-_a-z0-9]+@([a-z0-9][\\-a-z0-9]+\\.)+[a-z]{2,6}";
            Match isMatch = Regex.Match(email, pattern, RegexOptions.IgnoreCase);
            return isMatch.Success;
        }

        private List<Doctor> Doctors = new List<Doctor>();

        private Doctor CurrDoctor;

    }
}
