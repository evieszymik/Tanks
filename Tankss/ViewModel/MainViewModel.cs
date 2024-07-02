using MySql.Data.MySqlClient;
using MySqlX.XDevAPI.Common;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using Tankss.Model;

namespace Tankss.ViewModel
{
    public class MainViewModel : INotifyPropertyChanged
    {
        private MediaPlayer mediaPlayer;
        
        private ObservableCollection<Tank> _Tanks { get; set; }
        public ObservableCollection<string> Types { get; set; }
        public ObservableCollection<Tank> FilteredTanks { get; set; }
        private string _selectedType;
        public event PropertyChangedEventHandler PropertyChanged;
        public ICommand LoadTanksCommand { get; set; }
        public ICommand BrowseCommand { get; set; }
        public ICommand CompareCommand { get; set; }
        public ICommand CompareTanksCommand { get; set; }
        public ICommand ReturnCommand { get; set; }
        public ICommand InstructionCommand { get; set; }
        public ICommand Authors {  get; set; }
        public ICommand Selection {  get; set; }
        public ICommand ClearFilter { get; set; }
        public ObservableCollection<Tank> Tanks
        {
            get { return _Tanks; }
            set
            {
                _Tanks = value;
                onPropertyChanged("Tanks");
            }
        }
        private string result;

        public string Result
        {
            get { return result; }
            set
            {
                if (result != value)
                {
                    result = value;
                    onPropertyChanged(nameof(Result));
                }
            }
        }
        private Tank _selectedTank;
        private Tank _selectedTank1;
        private Tank _selectedTank2;
        private string _backgroundImageSource;
        public string SelectedTypeFilter
        {
            get { return _selectedType; }
            set
            {
                _selectedType = value;
                onPropertyChanged(nameof(SelectedTypeFilter));
                FilterTanks();
            }
        }


        public Tank SelectedTank
        {
            get { return _selectedTank; }
            set
            {
                
                _selectedTank = value;
                LoadTanks2();
                onPropertyChanged("SelectedTank");
                onPropertyChanged(nameof(SelectedArmyName));      
                onPropertyChanged(nameof(SelectedType));
                onPropertyChanged(nameof(SelectedArmor));
                onPropertyChanged(nameof(SelectedArmor1));
                onPropertyChanged(nameof(SelectedArmor2));
                onPropertyChanged(nameof(SelectedCannon));
                onPropertyChanged(nameof(SelectedCannon1));
                onPropertyChanged(nameof(SelectedCannon2));
                onPropertyChanged(nameof(SelectedCannon3));
                onPropertyChanged(nameof(SelectedEngine));
                onPropertyChanged(nameof(SelectedEngine1));
                onPropertyChanged(nameof(SelectedArmies));
                BackgroundImageSource = GetBackgroundImageForTank(_selectedTank?.Nazwa);


            }
        }
        public Tank SelectedTank1
        {
            get { return _selectedTank1; }
            set
            {
                _selectedTank1 = value;
                LoadTanks3();
                onPropertyChanged("SelectedTank");
                onPropertyChanged(nameof(CompareArmorFront1));
                onPropertyChanged(nameof(CompareArmorSide1));
                onPropertyChanged(nameof(CompareArmorBack1));
                onPropertyChanged(nameof(CompareEnginePower1));
                onPropertyChanged(nameof(CompareEngineSpeed1));
                onPropertyChanged(nameof(CompareCannonDamage1));
                onPropertyChanged(nameof(CompareCannonLoadTime1));
                onPropertyChanged(nameof(CompareCannonName1));
                onPropertyChanged(nameof(CompareCannonSpread1));
            }
        }
        public Tank SelectedTank2
        {
            get { return _selectedTank2; }
            set
            {
                _selectedTank2 = value;
                LoadTanks4();
                onPropertyChanged("SelectedTank");
                onPropertyChanged(nameof(CompareArmorFront2));
                onPropertyChanged(nameof(CompareArmorSide2));
                onPropertyChanged(nameof(CompareArmorBack2));
                onPropertyChanged(nameof(CompareEnginePower2));
                onPropertyChanged(nameof(CompareEngineSpeed2));
                onPropertyChanged(nameof(CompareCannonDamage2));
                onPropertyChanged(nameof(CompareCannonLoadTime2));
                onPropertyChanged(nameof(CompareCannonName2));              
                onPropertyChanged(nameof(CompareCannonSpread2));
            }
        }

        private string GetBackgroundImageForTank(string tankName)
        {

            switch (tankName)
            {
                case "T-34":
                    return "C:\\Users\\ewka0\\OneDrive\\Pulpit\\Studia\\Programowanie graficzne\\Pawlikowska_Stępień_Szymik_Projekt\\Zdjęcia Czołgów\\t-34.png";
                case "14TP":
                    return "C:\\Users\\ewka0\\OneDrive\\Pulpit\\Studia\\Programowanie graficzne\\Pawlikowska_Stępień_Szymik_Projekt\\Zdjęcia Czołgów\\14TP.png";
                case "Churchill":
                    return "C:\\Users\\ewka0\\OneDrive\\Pulpit\\Studia\\Programowanie graficzne\\Pawlikowska_Stępień_Szymik_Projekt\\Zdjęcia Czołgów\\Churchill.png";
                case "Sherman":
                    return "C:\\Users\\ewka0\\OneDrive\\Pulpit\\Studia\\Programowanie graficzne\\Pawlikowska_Stępień_Szymik_Projekt\\Zdjęcia Czołgów\\Sherman.png";
                case "Tiger":
                    return "C:\\Users\\ewka0\\OneDrive\\Pulpit\\Studia\\Programowanie graficzne\\Pawlikowska_Stępień_Szymik_Projekt\\Zdjęcia Czołgów\\Tiger.png";
                case "Panther":
                    return "C:\\Users\\ewka0\\OneDrive\\Pulpit\\Studia\\Programowanie graficzne\\Pawlikowska_Stępień_Szymik_Projekt\\Zdjęcia Czołgów\\Panther.png";
                case "B1":
                    return "C:\\Users\\ewka0\\OneDrive\\Pulpit\\Studia\\Programowanie graficzne\\Pawlikowska_Stępień_Szymik_Projekt\\Zdjęcia Czołgów\\B1.png";
                case "Pz4":
                    return "C:\\Users\\ewka0\\OneDrive\\Pulpit\\Studia\\Programowanie graficzne\\Pawlikowska_Stępień_Szymik_Projekt\\Zdjęcia Czołgów\\pz4.png";
                case "Chi-Ha":
                    return "C:\\Users\\ewka0\\OneDrive\\Pulpit\\Studia\\Programowanie graficzne\\Pawlikowska_Stępień_Szymik_Projekt\\Zdjęcia Czołgów\\Chi-ha.png";
                case "T1":
                    return "C:\\Users\\ewka0\\OneDrive\\Pulpit\\Studia\\Programowanie graficzne\\Pawlikowska_Stępień_Szymik_Projekt\\Zdjęcia Czołgów\\T1.png";
                default:
                    return "C:\\Users\\ewka0\\OneDrive\\Pulpit\\Studia\\Programowanie graficzne\\Pawlikowska_Stępień_Szymik_Projekt\\Zdjęcia Czołgów\\0c431a4b511c14153bf20b5a223d8dd3.jpg";
            }
        }
        public string SelectedArmyName => SelectedTank?.KrajPochodzenia;
        public string SelectedType => SelectedTank?.Typ;
        public int? SelectedArmor => SelectedTank?.Armor.Przod;
        public int? SelectedArmor1 => SelectedTank?.Armor.Bok;
        public int? SelectedArmor2 => SelectedTank?.Armor.Tyl;
        public string SelectedCannon => SelectedTank?.Cannon.Nazwa;
        public int? SelectedCannon1 => SelectedTank?.Cannon.SrUszkodzenia;
        public double? SelectedCannon2 => SelectedTank?.Cannon.CzasLad;
        public double? SelectedCannon3 => SelectedTank?.Cannon.Rozrzut;
        public int? SelectedEngine => SelectedTank?.Engine.moc;
        public int? SelectedEngine1 => SelectedTank?.Engine.pred_maks;
        public string SelectedArmies
        {
            get
            {
                if (SelectedTank != null && SelectedTank.Armies != null)
                {
                    return string.Join("  ", SelectedTank.Armies.Select(a => a.Nazwa));
                }
                return string.Empty;
            }
        }

        public string? CompareArmorFront1 => SelectedTank1?.Armor.Przod.ToString();
        public string? CompareArmorSide1 => SelectedTank1?.Armor.Bok.ToString();
        public string? CompareArmorBack1 => SelectedTank1?.Armor.Tyl.ToString();
        public string? CompareEnginePower1 => SelectedTank1?.Engine.moc.ToString();
        public string? CompareEngineSpeed1 => SelectedTank1?.Engine.pred_maks.ToString();
        public string? CompareCannonSpread1 => SelectedTank1?.Cannon.Rozrzut.ToString();
        public string? CompareCannonDamage1 => SelectedTank1?.Cannon.SrUszkodzenia.ToString();
        public string? CompareCannonName1 => SelectedTank1?.Cannon.Nazwa.ToString();
        public string? CompareCannonLoadTime1 => SelectedTank1?.Cannon.CzasLad.ToString();

        public string? CompareArmorFront2 => SelectedTank2?.Armor.Przod.ToString();
        public string? CompareArmorSide2 => SelectedTank2?.Armor.Bok.ToString();
        public string? CompareArmorBack2 => SelectedTank2?.Armor.Tyl.ToString();
        public string? CompareEnginePower2 => SelectedTank2?.Engine.moc.ToString();
        public string? CompareEngineSpeed2 => SelectedTank2?.Engine.pred_maks.ToString();
        public string? CompareCannonSpread2 => SelectedTank2?.Cannon.Rozrzut.ToString();
        public string? CompareCannonDamage2 => SelectedTank2?.Cannon.SrUszkodzenia.ToString();
        public string? CompareCannonName2 => SelectedTank2?.Cannon.Nazwa.ToString();
        public string? CompareCannonLoadTime2 => SelectedTank2?.Cannon.CzasLad.ToString();
        

        private void onPropertyChanged(string propertyName)
        {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName)); 
        }

        public string BackgroundImageSource
        {
            get { return _backgroundImageSource; }
            set
            {
                if (_backgroundImageSource != value)
                {
                    _backgroundImageSource = value;
                   onPropertyChanged(nameof(BackgroundImageSource));
                }
            }
        }
        public MainViewModel()
        {
            Tanks = new ObservableCollection<Tank>();
            FilteredTanks = new ObservableCollection<Tank>();
            Types = new ObservableCollection<string>();
            LoadTanksCommand = new RelayCommand(LoadTanks);
            BrowseCommand = new RelayCommand(Browse);
            CompareCommand = new RelayCommand(Compare);
            CompareTanksCommand = new RelayCommand(CompareTanks);
            ReturnCommand = new RelayCommand(Return);
            Authors = new RelayCommand(Author);
            Selection = new RelayCommand(sel);
            ClearFilter = new RelayCommand(Clear);
            InstructionCommand = new RelayCommand(Instruction);
            ColorArmorFront1 = new SolidColorBrush(Colors.Black);
            ColorArmorFront2 = new SolidColorBrush(Colors.Black);
            ColorArmorSide1 = new SolidColorBrush(Colors.Black);
            ColorArmorSide2 = new SolidColorBrush(Colors.Black);
            ColorArmorBack1 = new SolidColorBrush(Colors.Black);
            ColorArmorBack2 = new SolidColorBrush(Colors.Black);
            ColorEnginePower1 = new SolidColorBrush(Colors.Black);
            ColorEnginePower2 = new SolidColorBrush(Colors.Black);
            ColorEngineSpeed1 = new SolidColorBrush(Colors.Black);
            ColorEngineSpeed2 = new SolidColorBrush(Colors.Black);
            ColorCannonSpread1 = new SolidColorBrush(Colors.Black);
            ColorCannonSpread2 = new SolidColorBrush(Colors.Black);
            ColorCannonLoadTime1 = new SolidColorBrush(Colors.Black);
            ColorCannonLoadTime2 = new SolidColorBrush(Colors.Black);
            ColorCannonDamage1 = new SolidColorBrush(Colors.Black);
            ColorCannonDamage2 = new SolidColorBrush(Colors.Black);
   
            /*mediaPlayer = new MediaPlayer();
            mediaPlayer.Open(new Uri("C:/Users/wiktoria/Downloads/Tanks.mp3"));
            mediaPlayer.MediaOpened += MediaPlayer_MediaOpened;
            BackgroundImageSource = "C:/Users/wiktoria/Desktop/0c431a4b511c14153bf20b5a223d8dd3.jpg";
            mediaPlayer.Volume = 0.5;*/
            LoadTanks();
            LoadTypes();

        }

/*        private void MediaPlayer_MediaOpened(object sender, EventArgs e)
        {
            mediaPlayer.Play();
        }*/
         public void sel()
        {
            SelectedTank = null;
        }
        private void LoadTanks()
        {
            try
            {
                string connetionString = "Server=127.0.0.1;Database=tankbase;User Id=root;Password=Creepypasta256&;";
                using (MySqlConnection cnn = new MySqlConnection(connetionString))
                {

                    cnn.Open();
                    string polecenie = "select * from model";
                    MySqlCommand cmd = new MySqlCommand(polecenie, cnn);
                    MySqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        Tank tank = new Tank
                        {
                            ID = Convert.ToInt32(reader["id_czolg"]),
                            Nazwa = reader["nazwa"].ToString(),
                            Typ = reader["typ"].ToString(),
                            KrajPochodzenia = reader["kraj_pochodzenia"].ToString(),
                            BackgroundImageSource = GetBackgroundImageForTank(reader["nazwa"].ToString())
                        };
                        Tanks.Add(tank);
                    }

                    reader.Close();

                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
            FilterTanks();
        }
        private void LoadTypes()
        {
            
            var types = Tanks.Select(t => t.Typ).Distinct().ToList();
            foreach (var type in types)
            {
                Types.Add(type);
            }
        }
        private void FilterTanks()
        {
            FilteredTanks.Clear();
            BackgroundImageSource = "0c431a4b511c14153bf20b5a223d8dd3.jpg";
            var filtered = string.IsNullOrEmpty(SelectedTypeFilter) ?
                           Tanks :
                           Tanks.Where(t => t.Typ == SelectedTypeFilter);

            foreach (var tank in filtered)
            {
                FilteredTanks.Add(tank);
            }
        }
        public void Clear()
        {
            SelectedTypeFilter = null;
            BackgroundImageSource = "0c431a4b511c14153bf20b5a223d8dd3.jpg";
            FilterTanks();

        }

        private void LoadTanks2()
        {
            if (SelectedTank == null)
            {
                BackgroundImageSource = "0c431a4b511c14153bf20b5a223d8dd3.jpg";
                return;
            }


            try
            {
                string connectionString = "Server=127.0.0.1;Database=tankbase;User Id=root;Password=Creepypasta256&;";
                using (MySqlConnection cnn = new MySqlConnection(connectionString))
                {
                    cnn.Open();
                    string query = @"SELECT d.nazwa nazwa_d, d.sr_uszkodzenia uszkodzenia, d.czas_lad ladownosc, d.rozrzut rozrzut,
                             p.p_przod przod, p.p_bok bok, p.p_tyl tyl,
                             s.moc moc, s.pred_maks max,
                             a.nazwa nazwa_armii
                             FROM model m, dzialo d, pancerz p, silnik s, armia a, przynaleznosc pr where
                             m.id_dzialo = d.id_dzialo and
                             m.id_pancerz = p.id_pancerz and
                             m.id_silnik = s.id_silnik and
                             m.id_czolg = pr.id_czolg and
                             pr.id_armia = a.id_armia and
                             m.id_czolg = @selectedModelId";
                    MySqlCommand cmd = new MySqlCommand(query, cnn);
                    cmd.Parameters.AddWithValue("@selectedModelId", SelectedTank.ID);
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            SelectedTank.Cannon = new Cannon
                            {
                                Nazwa = reader["nazwa_d"].ToString(),
                                SrUszkodzenia = Convert.ToInt32(reader["uszkodzenia"]),
                                CzasLad = Convert.ToDouble(reader["ladownosc"]),
                                Rozrzut = Convert.ToDouble(reader["rozrzut"])
                            };

                            SelectedTank.Armor = new Armor
                            {
                                Przod = Convert.ToInt32(reader["przod"]),
                                Bok = Convert.ToInt32(reader["bok"]),
                                Tyl = Convert.ToInt32(reader["tyl"])
                            };

                            SelectedTank.Engine = new Engine
                            {
                                moc = Convert.ToInt32(reader["moc"]),
                                pred_maks = Convert.ToInt32(reader["max"])
                            };

                            SelectedTank.Armies.Clear();
                            do
                            {
                                Army army = new Army
                                {
                                    Nazwa = reader["nazwa_armii"].ToString()
                                };
                                SelectedTank.Armies.Add(army);
                            } while (reader.Read());
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading tank details: " + ex.Message);
            }
        }

        public void LoadTanks3()
        {
            try
            {
                string connectionString = "Server=127.0.0.1;Database=tankbase;User Id=root;Password=Creepypasta256&;";
                using (MySqlConnection cnn = new MySqlConnection(connectionString))
                {
                    cnn.Open();
                    string query = @"SELECT d.nazwa nazwa_d, d.sr_uszkodzenia uszkodzenia, d.czas_lad ladownosc, d.rozrzut rozrzut,
                                    p.p_przod przod, p.p_bok bok, p.p_tyl tyl,
                                    s.moc moc, s.pred_maks max,
                                    a.nazwa nazwa_armii
                            FROM model m, dzialo d, pancerz p, silnik s, armia a, przynaleznosc pr where
                            m.id_dzialo = d.id_dzialo and
                            m.id_pancerz = p.id_pancerz and
                            m.id_silnik = s.id_silnik and
                            m.id_czolg = pr.id_czolg and
                            pr.id_armia = a.id_armia and
                            m.id_czolg = @selectedModelId";
                    MySqlCommand cmd = new MySqlCommand(query, cnn);
                    cmd.Parameters.AddWithValue("@selectedModelId", SelectedTank1.ID);
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            SelectedTank1.Cannon = new Cannon
                            {
                                Nazwa = reader["nazwa_d"].ToString(),
                                SrUszkodzenia = Convert.ToInt32(reader["uszkodzenia"]),
                                CzasLad = Convert.ToDouble(reader["ladownosc"]),
                                Rozrzut = Convert.ToDouble(reader["rozrzut"])
                            };

                            SelectedTank1.Armor = new Armor
                            {
                                Przod = Convert.ToInt32(reader["przod"]),
                                Bok = Convert.ToInt32(reader["bok"]),
                                Tyl = Convert.ToInt32(reader["tyl"])
                            };

                            SelectedTank1.Engine = new Engine
                            {
                                moc = Convert.ToInt32(reader["moc"]),
                                pred_maks = Convert.ToInt32(reader["max"])
                            };

                            SelectedTank1.Armies.Clear();
                            do
                            {
                                Army army = new Army
                                {
                                    Nazwa = reader["nazwa_armii"].ToString()
                                };
                                SelectedTank1.Armies.Add(army);
                            } while (reader.Read());
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading tank details: " + ex.Message);
            }


        }

        public void LoadTanks4()
        {
            try
            {
                string connectionString = "Server=127.0.0.1;Database=tankbase;User Id=root;Password=Creepypasta256&;";
                using (MySqlConnection cnn = new MySqlConnection(connectionString))
                {
                    cnn.Open();
                    string query = @"SELECT d.nazwa nazwa_d, d.sr_uszkodzenia uszkodzenia, d.czas_lad ladownosc, d.rozrzut rozrzut,
                                    p.p_przod przod, p.p_bok bok, p.p_tyl tyl,
                                    s.moc moc, s.pred_maks max,
                                    a.nazwa nazwa_armii
                            FROM model m, dzialo d, pancerz p, silnik s, armia a, przynaleznosc pr where
                            m.id_dzialo = d.id_dzialo and
                            m.id_pancerz = p.id_pancerz and
                            m.id_silnik = s.id_silnik and
                            m.id_czolg = pr.id_czolg and
                            pr.id_armia = a.id_armia and
                            m.id_czolg = @selectedModelId";
                    MySqlCommand cmd = new MySqlCommand(query, cnn);
                    cmd.Parameters.AddWithValue("@selectedModelId", SelectedTank2.ID);
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            SelectedTank2.Cannon = new Cannon
                            {
                                Nazwa = reader["nazwa_d"].ToString(),
                                SrUszkodzenia = Convert.ToInt32(reader["uszkodzenia"]),
                                CzasLad = Convert.ToDouble(reader["ladownosc"]),
                                Rozrzut = Convert.ToDouble(reader["rozrzut"])
                            };

                            SelectedTank2.Armor = new Armor
                            {
                                Przod = Convert.ToInt32(reader["przod"]),
                                Bok = Convert.ToInt32(reader["bok"]),
                                Tyl = Convert.ToInt32(reader["tyl"])
                            };

                            SelectedTank2.Engine = new Engine
                            {
                                moc = Convert.ToInt32(reader["moc"]),
                                pred_maks = Convert.ToInt32(reader["max"])
                            };

                            SelectedTank2.Armies.Clear();
                            do
                            {
                                Army army = new Army
                                {
                                    Nazwa = reader["nazwa_armii"].ToString()
                                };
                                SelectedTank2.Armies.Add(army);
                            } while (reader.Read());
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading tank details: " + ex.Message);
            }


        }
        private void Browse()
        {
            Window_Browse browseWindow = new Window_Browse();
            Application.Current.MainWindow.Close();
            Application.Current.MainWindow = browseWindow;
            onPropertyChanged(nameof(BackgroundImageSource));
            browseWindow.Show();
        }
        private void Compare()
        {
            Window_Compare compareWindow = new Window_Compare();
            Application.Current.MainWindow.Close();
            compareWindow.Show();
            Application.Current.MainWindow = compareWindow;
        }
        private void Instruction()
        {
            Instruction instructionWindow = new Instruction();
            Application.Current.MainWindow.Close();
            instructionWindow.Show();
            Application.Current.MainWindow = instructionWindow;
        }

        private void Author()
        {
            Authors aut_win = new Authors();
            Application.Current.MainWindow.Close();
            aut_win.Show();
            Application.Current.MainWindow = aut_win;
        }
        private void Return()
        {
            MainWindow mainWindow = new MainWindow();
            
            Application.Current.MainWindow.Close();
            mainWindow.Show();
            Application.Current.MainWindow = mainWindow;
        }
        private void CompareTanks()
        {
            int left = 0;
            int right = 0;
            if (SelectedTank1 == null || SelectedTank2 == null)
            {
                MessageBox.Show("Nie wybrano elemetu");
            }
            else
            {


                if (SelectedTank1.Armor.Przod > SelectedTank2.Armor.Przod)
                {
                    ColorArmorFront1 = new SolidColorBrush(Colors.Green);
                    ColorArmorFront2 = new SolidColorBrush(Colors.Red);
                    left++;
                }
                else if (SelectedTank1.Armor.Przod < SelectedTank2.Armor.Przod)
                {
                    ColorArmorFront1 = new SolidColorBrush(Colors.Red);
                    ColorArmorFront2 = new SolidColorBrush(Colors.Green);
                    right++;
                }
                else
                {
                    ColorArmorFront1 = new SolidColorBrush(Colors.Black);
                    ColorArmorFront2 = new SolidColorBrush(Colors.Black);
                }


                if (SelectedTank1.Armor.Bok > SelectedTank2.Armor.Bok)
                {
                    ColorArmorSide1 = new SolidColorBrush(Colors.Green);
                    ColorArmorSide2 = new SolidColorBrush(Colors.Red);
                    left++;

                }
                else if (SelectedTank1.Armor.Bok < SelectedTank2.Armor.Bok)
                {
                    ColorArmorSide1 = new SolidColorBrush(Colors.Red);
                    ColorArmorSide2 = new SolidColorBrush(Colors.Green);
                    right++;

                }
                else
                {
                    ColorArmorSide1 = new SolidColorBrush(Colors.Black);
                    ColorArmorSide2 = new SolidColorBrush(Colors.Black);
                }


                if (SelectedTank1.Armor.Tyl > SelectedTank2.Armor.Tyl)
                {
                    ColorArmorBack1 = new SolidColorBrush(Colors.Green);
                    ColorArmorBack2 = new SolidColorBrush(Colors.Red);
                    left++;
                }
                else if (SelectedTank1.Armor.Tyl < SelectedTank2.Armor.Tyl)
                {
                    ColorArmorBack1 = new SolidColorBrush(Colors.Red);
                    ColorArmorBack2 = new SolidColorBrush(Colors.Green);
                    right++;
                }
                else
                {
                    ColorArmorBack1 = new SolidColorBrush(Colors.Black);
                    ColorArmorBack2 = new SolidColorBrush(Colors.Black);
                }


                if (SelectedTank1.Engine.pred_maks > SelectedTank2.Engine.pred_maks)
                {
                    ColorEngineSpeed1 = new SolidColorBrush(Colors.Green);
                    ColorEngineSpeed2 = new SolidColorBrush(Colors.Red);
                    left++;
                }
                else if (SelectedTank1.Engine.pred_maks < SelectedTank2.Engine.pred_maks)
                {
                    ColorEngineSpeed1 = new SolidColorBrush(Colors.Red);
                    ColorEngineSpeed2 = new SolidColorBrush(Colors.Green);
                    right++;
                }
                else
                {
                    ColorEngineSpeed1 = new SolidColorBrush(Colors.Black);
                    ColorEngineSpeed2 = new SolidColorBrush(Colors.Black);
                }


                if (SelectedTank1.Engine.moc > SelectedTank2.Engine.moc)
                {
                    ColorEnginePower1 = new SolidColorBrush(Colors.Green);
                    ColorEnginePower2 = new SolidColorBrush(Colors.Red);
                    left++;
                }
                else if (SelectedTank1.Engine.moc < SelectedTank2.Engine.moc)
                {
                    ColorEnginePower1 = new SolidColorBrush(Colors.Red);
                    ColorEnginePower2 = new SolidColorBrush(Colors.Green);
                    right++;
                }
                else
                {
                    ColorEnginePower1 = new SolidColorBrush(Colors.Black);
                    ColorEnginePower2 = new SolidColorBrush(Colors.Black);
                }


                if (SelectedTank1.Cannon.SrUszkodzenia > SelectedTank2.Cannon.SrUszkodzenia)
                {
                    ColorCannonDamage1 = new SolidColorBrush(Colors.Green);
                    ColorCannonDamage2 = new SolidColorBrush(Colors.Red);
                    left++;
                }
                else if (SelectedTank1.Cannon.SrUszkodzenia < SelectedTank2.Cannon.SrUszkodzenia)
                {
                    ColorCannonDamage1 = new SolidColorBrush(Colors.Red);
                    ColorCannonDamage2 = new SolidColorBrush(Colors.Green);
                    right++;
                }
                else
                {
                    ColorCannonDamage1 = new SolidColorBrush(Colors.Black);
                    ColorCannonDamage2 = new SolidColorBrush(Colors.Black);
                }


                if (SelectedTank1.Cannon.Rozrzut > SelectedTank2.Cannon.Rozrzut)
                {
                    ColorCannonSpread1 = new SolidColorBrush(Colors.Green);
                    ColorCannonSpread2 = new SolidColorBrush(Colors.Red);
                    left++;
                }
                else if (SelectedTank1.Cannon.Rozrzut < SelectedTank2.Cannon.Rozrzut)
                {
                    ColorCannonSpread1 = new SolidColorBrush(Colors.Red);
                    ColorCannonSpread2 = new SolidColorBrush(Colors.Green);
                    right++;
                }
                else
                {
                    ColorCannonSpread1 = new SolidColorBrush(Colors.Black);
                    ColorCannonSpread2 = new SolidColorBrush(Colors.Black);
                }


                if (SelectedTank1.Cannon.CzasLad < SelectedTank2.Cannon.CzasLad)
                {
                    ColorCannonLoadTime1 = new SolidColorBrush(Colors.Green);
                    ColorCannonLoadTime2 = new SolidColorBrush(Colors.Red);
                    left++;
                }
                else if (SelectedTank1.Cannon.CzasLad > SelectedTank2.Cannon.CzasLad)
                {
                    ColorCannonLoadTime1 = new SolidColorBrush(Colors.Red);
                    ColorCannonLoadTime2 = new SolidColorBrush(Colors.Green);
                    right++;
                }
                else
                {
                    ColorCannonLoadTime1 = new SolidColorBrush(Colors.Black);
                    ColorCannonLoadTime2 = new SolidColorBrush(Colors.Black);
                }
                result = $"{left}:{right}";
                onPropertyChanged(nameof(Result));
            }
        }



        private SolidColorBrush colorArmorFront1;
        private SolidColorBrush colorArmorFront2;
        private SolidColorBrush colorArmorSide1;
        private SolidColorBrush colorArmorSide2;
        private SolidColorBrush colorArmorBack1;
        private SolidColorBrush colorArmorBack2;
        private SolidColorBrush colorEngineSpeed1;
        private SolidColorBrush colorEngineSpeed2;
        private SolidColorBrush colorEnginePower1;
        private SolidColorBrush colorEnginePower2;
        private SolidColorBrush colorCannonDamage1;
        private SolidColorBrush colorCannonDamage2;
        private SolidColorBrush colorCannonLoadTime1;
        private SolidColorBrush colorCannonLoadTime2;
        private SolidColorBrush colorCannonSpread2;
        private SolidColorBrush colorCannonSpread1;


        public SolidColorBrush ColorArmorFront1
        {
            get { return colorArmorFront1; }
            set
            {
                if (colorArmorFront1 != value)
                {
                    colorArmorFront1 = value;
                    onPropertyChanged(nameof(ColorArmorFront1));
                }
            }
        }
        public SolidColorBrush ColorArmorFront2
        {
            get { return colorArmorFront2; }
            set
            {
                if (colorArmorFront2 != value)
                {
                    colorArmorFront2 = value;
                    onPropertyChanged(nameof(ColorArmorFront2));
                }
            }
        }
        public SolidColorBrush ColorArmorSide1
        {
            get { return colorArmorSide1; }
            set
            {
                if (colorArmorSide1 != value)
                {
                    colorArmorSide1 = value;
                    onPropertyChanged(nameof(ColorArmorSide1));
                }
            }
        }
        public SolidColorBrush ColorArmorSide2
        {
            get { return colorArmorSide2; }
            set
            {
                if (colorArmorSide2 != value)
                {
                    colorArmorSide2 = value;
                    onPropertyChanged(nameof(ColorArmorSide2));
                }
            }
        }
        public SolidColorBrush ColorArmorBack1
        {
            get { return colorArmorBack1; }
            set
            {
                if (colorArmorBack1 != value)
                {
                    colorArmorBack1 = value;
                    onPropertyChanged(nameof(ColorArmorBack1));
                }
            }
        }
        public SolidColorBrush ColorArmorBack2
        {
            get { return colorArmorBack2; }
            set
            {
                if (colorArmorBack2 != value)
                {
                    colorArmorBack2 = value;
                    onPropertyChanged(nameof(ColorArmorBack2));
                }
            }
        }
        public SolidColorBrush ColorEngineSpeed1
        {
            get { return colorEngineSpeed1; }
            set
            {
                if (colorEngineSpeed1 != value)
                {
                    colorEngineSpeed1 = value;
                    onPropertyChanged(nameof(ColorEngineSpeed1));
                }
            }
        }
        public SolidColorBrush ColorEngineSpeed2
        {
            get { return colorEngineSpeed2; }
            set
            {
                if (colorEngineSpeed2 != value)
                {
                    colorEngineSpeed2 = value;
                    onPropertyChanged(nameof(ColorEngineSpeed2));
                }
            }
        }
        public SolidColorBrush ColorEnginePower1
        {
            get { return colorEnginePower1; }
            set
            {
                if (colorEnginePower1 != value)
                {
                    colorEnginePower1 = value;
                    onPropertyChanged(nameof(ColorEnginePower1));
                }
            }
        }
        public SolidColorBrush ColorEnginePower2
        {
            get { return colorEnginePower2; }
            set
            {
                if (colorEnginePower2 != value)
                {
                    colorEnginePower2 = value;
                    onPropertyChanged(nameof(ColorEnginePower2));
                }
            }
        }
        public SolidColorBrush ColorCannonDamage1
        {
            get { return colorCannonDamage1; }
            set
            {
                if (colorCannonDamage1 != value)
                {
                    colorCannonDamage1 = value;
                    onPropertyChanged(nameof(ColorCannonDamage1));
                }
            }
        }
        public SolidColorBrush ColorCannonDamage2
        {
            get { return colorCannonDamage2; }
            set
            {
                if (colorCannonDamage2 != value)
                {
                    colorCannonDamage2 = value;
                    onPropertyChanged(nameof(ColorCannonDamage2));
                }
            }
        }
        public SolidColorBrush ColorCannonLoadTime1
        {
            get { return colorCannonLoadTime1; }
            set
            {
                if (colorCannonLoadTime1 != value)
                {
                    colorCannonLoadTime1 = value;
                    onPropertyChanged(nameof(ColorCannonLoadTime1));
                }
            }
        }
        public SolidColorBrush ColorCannonLoadTime2
        {
            get { return colorCannonLoadTime2; }
            set
            {
                if (colorCannonLoadTime2 != value)
                {
                    colorCannonLoadTime2 = value;
                    onPropertyChanged(nameof(ColorCannonLoadTime2));
                }
            }
        }
        public SolidColorBrush ColorCannonSpread1
        {
            get { return colorCannonSpread1; }
            set
            {
                if (colorCannonSpread1 != value)
                {
                    colorCannonSpread1 = value;
                    onPropertyChanged(nameof(ColorCannonSpread1));
                }
            }
        }
        public SolidColorBrush ColorCannonSpread2
        {
            get { return colorCannonSpread2; }
            set
            {
                if (colorCannonSpread2 != value)
                {
                    colorCannonSpread2 = value;
                    onPropertyChanged(nameof(ColorCannonSpread2));
                }
            }
        }


    }
}
