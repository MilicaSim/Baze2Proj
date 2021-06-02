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
    public class GlumacViewModel : BindableBase
    {
        private bool isVisibleModifikuj;
        private bool isVisibleObrisi;
        private bool isVisibleDodaj;
        private bool isVisibleStek;

        private ObservableCollection<Film> filmList = new ObservableCollection<Film>();
        private Film currentFilm;
        private Dictionary<string, bool> filmoviBool = new Dictionary<string, bool>();
        private ICommand visibilityCheckedCommand;
        private ICommand visibilityUncheckedCommand;

        private Glumac glumacMD;
        private Glumac selektovaniGlumac;
        public BindingList<Glumac> glumci;

        public ICommand NavCommand { get; private set; }
        public ICommand DodajCommand { get; private set; }
        public ICommand ModifikujCommand { get; private set; }
        public ICommand ObrisiCommand { get; private set; }


        public GlumacViewModel()
        {
            IsVisibleModifikuj = false;
            IsVisibleDodaj = false;
            IsVisibleObrisi = false;
            IsVisibleStek = false;

            NavCommand = new MyICommand<string>(OnNav);
            DodajCommand = new MyICommand(OnDodaj);
            ModifikujCommand = new MyICommand(OnModifikuj);
            ObrisiCommand = new MyICommand(OnObrisi);

            GlumacMD = new Glumac();
            SelektovaniGlumac = new Glumac();
            Glumci = GetAll();
            FilmList = GetFilms();
        }

        public BindingList<Glumac> GetAll()
        {
            using (var access = new ModelContainer())
            {
                var vl = access.Glumacs.ToList();

                BindingList<Glumac> vs = new BindingList<Glumac>();


                foreach (var v in vl)
                {
                    vs.Add(v);
                }
                return vs;
            }
        }
        public ObservableCollection<Film> GetFilms()
        {
            using (var access = new ModelContainer())
            {
                var vl = access.Films;

                ObservableCollection<Film> vs = new ObservableCollection<Film>();

                foreach (var v in vl)
                {
                    filmoviBool.Add(v.Naziv, false);
                    vs.Add(v);
                }
                return vs;
            }


        }


        public void OnDodaj()
        {
            using (var access = new ModelContainer())
            {
                int name = 0;
                bool suc = false;
                try
                {
                    foreach (var item in FilmoviBool)
                    {
                        if (item.Value)
                        {
                            var film = access.Films.FirstOrDefault(x => x.Naziv == item.Key);
                            GlumacMD.Films.Add(film);
                            GlumacMD.BrojUloga++;
                        }
                    }

                    #region Validation
                    if (glumacMD.Ime == null)
                    {
                        MessageBox.Show("Polje ime mora biti popunjeno!");
                        return;
                    }
                    else if (glumacMD.Prezime == null)
                    {
                        MessageBox.Show("Polje prezime mora biti popunjeno!");
                        return;
                    }
                    else if (glumacMD.BrojUloga < 0)
                    {
                        MessageBox.Show("Polje Broj uloga mora biti vece od 0!");
                        return;
                    }
                    else
                    {
                        try
                        {
                            name = Int32.Parse(glumacMD.Ime);
                            suc = true;
                            if (suc)
                            {
                                MessageBox.Show("Polje ime ne smije biti popunjeno brojevima!");
                                suc = false;
                                return;
                            }

                            try
                            {
                                name = Int32.Parse(glumacMD.Prezime);
                                suc = true;
                                if (suc)
                                {
                                    MessageBox.Show("Polje prezime ne smije biti popunjeno brojevima!");
                                    suc = false;
                                    return;
                                }

                                if (glumacMD.BrojUloga < 0)
                                {
                                    MessageBox.Show("Polje Broj uloga ne smije biti manje od 0!");
                                    return;

                                }

                            }
                            catch
                            {
                                if (glumacMD.BrojUloga < 0)
                                {
                                    MessageBox.Show("Polje Broj uloga ne smije biti manje od 0!");
                                    return;

                                }
                            }

                        }
                        catch
                        {
                            try
                            {
                                name = Int32.Parse(glumacMD.Prezime);
                                suc = true;
                                if (suc)
                                {
                                    MessageBox.Show("Polje prezime ne smije biti popunjeno brojevima!");
                                    suc = false;
                                    return;
                                }
                                if (glumacMD.BrojUloga < 0)
                                {
                                    MessageBox.Show("Polje Broj uloga ne smije biti manje od 0!");
                                    return;

                                }

                            }
                            catch
                            {
                                if (glumacMD.BrojUloga < 0)
                                {
                                    MessageBox.Show("Polje Broj uloga ne smije biti manje od 0!");
                                    return;

                                }
                            }
                        }
                    }
                    #endregion
                    //mora odabrati film
                    if (GlumacMD.Films.Count == 0)
                    {
                        MessageBox.Show("Mora se odabrati bar jedan film!");
                        return;

                    }


                    access.Glumacs.Add(glumacMD);

                    int success = access.SaveChanges();
                    if (success > 0)
                    {
                        MessageBox.Show("Uspijesno dodat objekat!");
                    }

                    GlumacMD = new Glumac();
                    Glumci = GetAll();
                }
                catch (DbUpdateException)
                {
                    MessageBox.Show("Sifra radnika vec postoji!");
                }

            }
        }
        public void OnModifikuj()
        {
            int name = 0;
            bool suc = false;
            using (var access = new ModelContainer())
            {
                try
                {
                    #region Validation
                    if (glumacMD.Ime == null)
                    {
                        MessageBox.Show("Polje ime mora biti popunjeno!");
                        return;
                    }
                    else if (glumacMD.Prezime == null)
                    {
                        MessageBox.Show("Polje prezime mora biti popunjeno!");
                        return;
                    }
                    else if (glumacMD.BrojUloga < 0)
                    {
                        MessageBox.Show("Polje Broj uloga mora biti vece od 0!");
                        return;
                    }
                    else
                    {
                        try
                        {
                            name = Int32.Parse(glumacMD.Ime);
                            suc = true;
                            if (suc)
                            {
                                MessageBox.Show("Polje ime ne smije biti popunjeno brojevima!");
                                suc = false;
                                return;
                            }

                            try
                            {
                                name = Int32.Parse(glumacMD.Prezime);
                                suc = true;
                                if (suc)
                                {
                                    MessageBox.Show("Polje prezime ne smije biti popunjeno brojevima!");
                                    suc = false;
                                    return;
                                }

                                if (glumacMD.BrojUloga < 0)
                                {
                                    MessageBox.Show("Polje Broj uloga ne smije biti manje od 0!");
                                    return;

                                }

                            }
                            catch
                            {
                                if (glumacMD.BrojUloga < 0)
                                {
                                    MessageBox.Show("Polje Broj uloga ne smije biti manje od 0!");
                                    return;

                                }
                            }

                        }
                        catch
                        {
                            try
                            {
                                name = Int32.Parse(glumacMD.Prezime);
                                suc = true;
                                if (suc)
                                {
                                    MessageBox.Show("Polje prezime ne smije biti popunjeno brojevima!");
                                    suc = false;
                                    return;
                                }
                                if (glumacMD.BrojUloga < 0)
                                {
                                    MessageBox.Show("Polje Broj uloga ne smije biti manje od 0!");
                                    return;

                                }

                            }
                            catch
                            {
                                if (glumacMD.BrojUloga < 0)
                                {
                                    MessageBox.Show("Polje Broj uloga ne smije biti manje od 0!");
                                    return;

                                }
                            }
                        }
                    }
                    #endregion
                    access.Glumacs.FirstOrDefault(n => n.IdGlumca == SelektovaniGlumac.IdGlumca).BrojUloga = GlumacMD.BrojUloga;
                    access.Glumacs.FirstOrDefault(n => n.IdGlumca == SelektovaniGlumac.IdGlumca).Ime = GlumacMD.Ime;
                    access.Glumacs.FirstOrDefault(n => n.IdGlumca == SelektovaniGlumac.IdGlumca).Prezime = GlumacMD.Prezime;
                    //films

                    int success = access.SaveChanges();
                    if (success > 0)
                    {
                        MessageBox.Show("Uspesno modifikovan objekat!");
                    }

                    Glumci = GetAll();

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
                    var glumac = access.Glumacs.FirstOrDefault(n => n.IdGlumca == SelektovaniGlumac.IdGlumca);
                    glumac.Films.Clear();
                    access.Glumacs.Remove(glumac);

                    int success = access.SaveChanges();
                    if (success > 0)
                    {
                        MessageBox.Show("Uspesno obrisan objekat!");
                    }

                    Glumci = GetAll();
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
                    GlumacMD = new Glumac();
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
                    GlumacMD = SelektovaniGlumac;
                    //if (GlumacMD != null)
                    //{
                    //    RadnikMD = GetAllRadnici().Where(n => n.IdRadnika == BlagajnikMD.IdRadnika).FirstOrDefault();
                    //}
                    //else
                    //{
                    //    RadnikMD = new Radnik();
                    //}

                    break;
            }
        }
        


        #region Property
        public Glumac GlumacMD
        {
            get { return glumacMD; }
            set
            {

                if (value != glumacMD)
                {
                    glumacMD = value;
                    OnPropertyChanged("GlumacMD");
                }
            }
        }
        public Glumac SelektovaniGlumac
        {
            get { return selektovaniGlumac; }
            set
            {

                if (value != selektovaniGlumac)
                {
                    selektovaniGlumac = value;
                    OnPropertyChanged("SelektovaniGlumac");
                    if (IsVisibleModifikuj == true)
                    {
                        GlumacMD = SelektovaniGlumac;
                        if (GlumacMD != null)
                        {
                            //RadnikMD = GetAllRadnici().Where(n => n.IdRadnika == GlumacMD.IdGlumca).FirstOrDefault();
                        }
                        else
                        {
                            GlumacMD = new Glumac();
                        }
                    }
                }
            }
        }
        public BindingList<Glumac> Glumci
        {
            get { return glumci; }
            set
            {

                if (value != glumci)
                {
                    glumci = value;
                    OnPropertyChanged("Glumci");
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
        public ObservableCollection<Film> FilmList
        {
            get => filmList;
            set
            {
                if (filmList == value)
                    return;
                filmList = value;
                OnPropertyChanged("FilmList");
            }
        }

        public Film CurrentFilm
        {
            get => currentFilm;
            set
            {
                if (currentFilm == value)
                    return;
                currentFilm = value;
                OnPropertyChanged("CurrentFilm");
            }
        }

        public Dictionary<string, bool> FilmoviBool { get => filmoviBool; set => filmoviBool = value; }

        public ICommand VisibilityCheckedCommand => visibilityCheckedCommand ?? (visibilityCheckedCommand = new MyICommand<KeyValuePair<string, bool>>(VisibilityCheckedCommandExecute));

        public ICommand VisibilityUncheckedCommand => visibilityUncheckedCommand ?? (visibilityUncheckedCommand = new MyICommand<KeyValuePair<string, bool>>(VisibilityUncheckedCommandExecute));

        private void VisibilityCheckedCommandExecute(KeyValuePair<string, bool> name)
        {
            FilmoviBool[name.Key] = true;
            OnPropertyChanged("FilmoviBool");
        }
        private void VisibilityUncheckedCommandExecute(KeyValuePair<string, bool> name)
        {
            FilmoviBool[name.Key] = false;
            OnPropertyChanged("FilmoviBool");
        }
        #endregion

    }
}

