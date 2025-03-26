using ECDL.Models;
using System.Collections.ObjectModel;
using System.DirectoryServices.ActiveDirectory;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel;
using System;

namespace ECDL
{
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public event PropertyChangedEventHandler? PropertyChanged;
        private DatabaseContext context = new DatabaseContext();
        public ObservableCollection<VizsgatipusModel> Vizsgatipusok { get; set; } = new ObservableCollection<VizsgatipusModel>();
        public ObservableCollection<VizsgazoModel> Vizsgazok { get; set; } = new ObservableCollection<VizsgazoModel>();
        private VizsgatipusModel selelectedType { get; set; }
        public VizsgatipusModel SelectedType
        {
            get { return selelectedType; }
            set { selelectedType = value; OnPropertyChanged(nameof(SelectedType)); }
        }

        private VizsgazoModel selectedVizsgazo;
        public VizsgazoModel SelectedVizsgazo
        {
            get { return selectedVizsgazo; }
            set { selectedVizsgazo = value; OnPropertyChanged(nameof(SelectedVizsgazo)); }
        }

        public MainWindow()
        {
            InitializeComponent();
            context.vizsgazok.Load();
            context.vizsgazotipusok.Load();
            Vizsgatipusok = context.vizsgazotipusok.Local.ToObservableCollection();
            Vizsgazok = context.vizsgazok.Local.ToObservableCollection();
            this.DataContext = this;
        }

        private void Torles_Button_Click(object sender, RoutedEventArgs e)
        {
            if (SelectedVizsgazo != null)
            {
                if (MessageBox.Show($"Biztosan törölni kívánka a(z) {selectedVizsgazo.Nev} nevű vizsgázót?", "Megerősítés", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    context.vizsgazok.Remove(SelectedVizsgazo);
                    context.SaveChanges();
                }
                return;
            }
            MessageBox.Show("Nincs kiválasztva törlendő elem!", "Hiba!", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        private void Mentes_Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(Nev_TextBlock.Text) &&
                    VizsgaT_ComboBox.SelectedItem is VizsgatipusModel selectedType &&
                    int.TryParse(Eredmeny_TextBox.Text, out int eredmeny))
                {
                    var newVizsgazo = new VizsgazoModel
                    {
                        Nev = Nev_TextBlock.Text,
                        Vizsgatipus = selectedType,
                        Eredmeny = eredmeny
                    };

                    context.vizsgazok.Add(newVizsgazo);
                    context.SaveChanges();

                    Nev_TextBlock.Text = string.Empty;
                    Eredmeny_TextBox.Text = string.Empty;
                    VizsgaT_ComboBox.SelectedIndex = -1;

                    MessageBox.Show("Új vizsgázó hozzáadva!", "Siker", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    MessageBox.Show("Kérjük, töltse ki az összes mezőt helyesen!", "Figyelmeztetés", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Hiba történt: {ex.Message}", "Hiba", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }


    }
}