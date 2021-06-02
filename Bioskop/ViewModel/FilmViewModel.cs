using Bioskop.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Bioskop.ViewModel
{
    public class FilmViewModel : BindableBase
    {
        private bool isVisibleModifikuj;
        private bool isVisibleObrisi;
        private bool isVisibleDodaj;
        private bool isVisibleStek;
        
        private Film filmMD;
        private Film selektovaniFilm;
        private BindingList<Film> filmovi;
        private BindingList<int> repertoari;
        private Repertoar _selectedRepertoar;
        private ObservableCollection<Repertoar> _listRepertoari = new ObservableCollection<Repertoar>();

        public ICommand NavCommand { get; private set; }
        public ICommand DodajCommand { get; private set; }
        public ICommand ModifikujCommand { get; private set; }
        public ICommand ObrisiCommand { get; private set; }

        public FilmViewModel()
        {
            IsVisibleModifikuj = false;
            IsVisibleDodaj = false;
            IsVisibleObrisi = false;
            IsVisibleStek = false;

            NavCommand = new MyICommand<string>(OnNav);
            DodajCommand = new MyICommand(OnDodaj);
            ModifikujCommand = new MyICommand(OnModifikuj);
            ObrisiCommand = new MyICommand(OnObrisi);

            FilmMD = new Film();
            SelektovaniFilm = new Film();
            Repertoari = GetAllRepertoari();
            Filmovi = GetAll();
        }
        public BindingList<int> GetAllRepertoari()
        {
            _listRepertoari.Clear();
            using (var access = new ModelContainer())
            {
                var vl = access.Repertoars;

                BindingList<int> vs = new BindingList<int>();
                foreach (var v in vl)
                {
                    _listRepertoari.Add(v);
                    vs.Add(v.IdRepertoara);
                }
                return vs;
            }
        }
        public BindingList<Film> GetAll()
        {
            using (var access = new ModelContainer())
            {
                var vl = access.Films;
                BindingList<Film> vs = new BindingList<Film>();
                foreach (var v in vl)
                {
                    vs.Add(v);

                }
                return vs;
            }
        }
        public void OnDodaj()
        {
            using (var access = new ModelContainer())
            {
                int zanr = 0;
                bool suc = false;
                try
                {
                    
                    #region Validation
                    if (FilmMD.Naziv==null)
                    {
                        MessageBox.Show("Polje naziv mora biti popunjeno!");
                        return;
                    }
                    else if (FilmMD.Trajanje <= 0)
                    {
                        MessageBox.Show("Polje Trajanje mora biti popunjeno!");
                        return;
                    }
                    else if (FilmMD.Zanr == null)
                    {
                        MessageBox.Show("Polje zanr mora biti popunjeno!");
                        return;
                    }
                    else
                    {
                        try
                        {
                            zanr = Int32.Parse(FilmMD.Zanr);
                            suc = true;
                            if (suc)
                            {
                                MessageBox.Show("Polje zanr ne smije biti popunjeno brojevima!");
                                suc = false;
                                return;
                            }
                        }
                        catch
                        {
                            foreach (var film in Filmovi)
                            {
                                if (film.Naziv.Equals(FilmMD.Naziv))
                                {
                                    MessageBox.Show("Film sa tim imenom vec postoji!");
                                    return;
                                }
                            }

                        }
                    }
                    #endregion

                    FilmMD.Repertoar = SelectedRepertoar;
                    access.Films.Add(FilmMD);


                    int success = access.SaveChanges();
                    if (success > 0)
                    {
                        MessageBox.Show("Uspesno dodat objekat!");
                    }

                    Filmovi = GetAll();
                    FilmMD = new Film();
                }
                catch (DbUpdateException)
                {
                    MessageBox.Show("Postoji objekat sa ovom kombinacijom kljuceva: broj sedista+broj vagona!");
                }

            }
        }
        public void OnModifikuj()
        {
            using (var access = new ModelContainer())
            {
                int zanr = 0;
                bool suc = false;
                try
                {
                    #region Validation
                    if (FilmMD.Naziv == null)
                    {
                        MessageBox.Show("Polje naziv mora biti popunjeno!");
                        return;
                    }
                    else if (FilmMD.Trajanje <= 0)
                    {
                        MessageBox.Show("Polje Trajanje mora biti popunjeno!");
                        return;
                    }
                    else if (FilmMD.Zanr == null)
                    {
                        MessageBox.Show("Polje zanr mora biti popunjeno!");
                        return;
                    }
                    else
                    {
                        try
                        {
                            zanr = Int32.Parse(FilmMD.Zanr); suc = true;
                            if (suc)
                            {
                                MessageBox.Show("Polje zanr ne smije biti popunjeno brojevima!");
                                suc = false;
                                return;
                            }
                        }
                        catch
                        {

                        }
                    }
                    #endregion

                    access.Films.FirstOrDefault(n => n.IdFilma == SelektovaniFilm.IdFilma).Naziv = FilmMD.Naziv;
                    access.Films.FirstOrDefault(n => n.IdFilma == SelektovaniFilm.IdFilma).Trajanje = FilmMD.Trajanje;
                    access.Films.FirstOrDefault(n => n.IdFilma == SelektovaniFilm.IdFilma).Zanr = FilmMD.Zanr;
                    access.Films.FirstOrDefault(n => n.IdFilma == SelektovaniFilm.IdFilma).RepertoarIdRepertoara = FilmMD.RepertoarIdRepertoara;


                    int success = access.SaveChanges();
                    if (success > 0)
                    {
                        MessageBox.Show("Uspesno modifikovan objekat!");
                    }

                    Filmovi = GetAll();

                }
                catch (Exception)
                {
                    MessageBox.Show("Ne mozete modifikovati objekat jer niste odabrali objekat!");
                }
            }
        }
        public void OnObrisi()
        {
            using (var access = new ModelContainer())
            {
                try
                {
                    var film = access.Films.FirstOrDefault(n => n.IdFilma == SelektovaniFilm.IdFilma);
                    film.Glumacs.Clear();

                    var prikazivanje = access.Prikazujes.FirstOrDefault(x => x.FilmIdFilma == film.IdFilma);
                   

                    if (prikazivanje != null)
                    {
                        var karta = access.Kartas.FirstOrDefault(x => x.PrikazujeIdPrikazivanja == prikazivanje.IdPrikazivanja);
                        if (karta != null)
                        {
                            access.Kartas.Remove(karta);
                            access.Prikazujes.Remove(prikazivanje);
                            access.Films.Remove(film);

                        }
                        else
                        {
                            access.Prikazujes.Remove(prikazivanje);
                            access.Films.Remove(film);
                        }
                    }
                    else
                    {
                        access.Films.Remove(film);
                    }
                    
                    int success = access.SaveChanges();
                    if (success > 0)
                    {
                        MessageBox.Show("Uspesno obrisan objekat!");
                    }

                    Filmovi = GetAll();
                }
                catch (ArgumentNullException)
                {
                    MessageBox.Show("Ne mozete obrisati objekat jer niste odabrali objekat!");
                }
                catch (TargetException)
                {
                    MessageBox.Show("Ne mozete obrisati objekat jer niste odabrali objekat!");
                }
                catch (DbUpdateException)
                {
                    MessageBox.Show("Ne mozete obrisati objekat jer je uvezan objekat!");
                }

            }
        }

        public void OnNav(object show)
        {

            string showView1 = (string)show;
            switch (showView1)
            {
                case "dodaj":
                    IsVisibleDodaj = true;
                    IsVisibleModifikuj = false;
                    IsVisibleObrisi = false;
                    IsVisibleStek = true;
                    FilmMD = new Film();
                    break;
                case "obrisi":
                    IsVisibleDodaj = false;
                    IsVisibleModifikuj = false;
                    IsVisibleObrisi = true;
                    IsVisibleStek = false;
                    break;
                case "modifikuj":
                    IsVisibleDodaj = false;
                    IsVisibleModifikuj = true;
                    IsVisibleObrisi = false;
                    IsVisibleStek = true;
                    FilmMD = SelektovaniFilm;
                 
                    break;
            }
        }

        #region Property
        public Film FilmMD
        {
            get { return filmMD; }
            set
            {

                if (value != filmMD)
                {
                    filmMD = value;
                    OnPropertyChanged("FilmMD");
                }
            }
        }
        public Film SelektovaniFilm
        {
            get { return selektovaniFilm; }
            set
            {

                if (value != selektovaniFilm)
                {
                    selektovaniFilm = value;
                    OnPropertyChanged("SelektovaniFilm");
                    FilmMD = SelektovaniFilm;
                   
                }
            }
        }
        public BindingList<Film> Filmovi
        {
            get { return filmovi; }
            set
            {

                if (value != filmovi)
                {
                    filmovi = value;
                    OnPropertyChanged("Filmovi");
                }
            }
        }

        public BindingList<int> Repertoari
        {
            get { return repertoari; }
            set
            {

                if (value != repertoari)
                {
                    repertoari = value;
                    OnPropertyChanged("Repertoari");
                }
            }
        }
        public bool IsVisibleDodaj
        {
            get { return isVisibleDodaj; }
            set
            {
                isVisibleDodaj = value;
                OnPropertyChanged("IsVisibleDodaj");
            }
        }
        public bool IsVisibleStek
        {
            get { return isVisibleStek; }
            set
            {
                isVisibleStek = value;
                OnPropertyChanged("IsVisibleStek");
            }
        }
        public bool IsVisibleObrisi
        {
            get { return isVisibleObrisi; }
            set
            {
                isVisibleObrisi = value;
                OnPropertyChanged("IsVisibleObrisi");
            }
        }
        public bool IsVisibleModifikuj
        {
            get { return isVisibleModifikuj; }
            set
            {
                isVisibleModifikuj = value;
                OnPropertyChanged("IsVisibleModifikuj");
            }
        }

        public Repertoar SelectedRepertoar { get => _selectedRepertoar; set => _selectedRepertoar = value; }
        public ObservableCollection<Repertoar> ListRepertoari { get => _listRepertoari; set => _listRepertoari = value; }


        #endregion
    }
}
