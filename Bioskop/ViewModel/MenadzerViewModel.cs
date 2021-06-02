using Bioskop.Model;
using System;
using System.Collections.Generic;
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
    public class MenadzerViewModel : BindableBase
    {
        private bool isVisibleModifikuj;
        private bool isVisibleObrisi;
        private bool isVisibleDodaj;
        private bool isVisibleStek;

        private Menadzer menadzerMD;
        private Radnik radnikMD;
        private Menadzer selektovaniMenadzer;
        public BindingList<Menadzer> menadzeri;

        public ICommand NavCommand { get; private set; }
        public ICommand DodajCommand { get; private set; }
        public ICommand ModifikujCommand { get; private set; }
        public ICommand ObrisiCommand { get; private set; }
        
        public MenadzerViewModel()
        {
            IsVisibleModifikuj = false;
            IsVisibleDodaj = false;
            IsVisibleObrisi = false;
            IsVisibleStek = false;

            NavCommand = new MyICommand<string>(OnNav);
            DodajCommand = new MyICommand(OnDodaj);
            ModifikujCommand = new MyICommand(OnModifikuj);
            ObrisiCommand = new MyICommand(OnObrisi);

            MenadzerMD = new Menadzer();
            SelektovaniMenadzer = new Menadzer();
            RadnikMD = new Radnik();
            Menadzeri = GetAll();
        }

        public BindingList<Radnik> GetAllRadnici()
        {
            using (var access = new ModelContainer())
            {
                var vl = access.Radniks;

                BindingList<Radnik> vs = new BindingList<Radnik>();
                foreach (var v in vl)
                {
                    vs.Add(v);
                }
                return vs;
            }
        }
        public BindingList<Menadzer> GetAll()
        {
            using (var access = new ModelContainer())
            {
                var vl = access.Radniks_Menadzer;
                
                BindingList<Menadzer> vs = new BindingList<Menadzer>();

                foreach (var v in vl)
                {
                    vs.Add(v);
                }
                return vs;
            }
        }

        public void OnDodaj()
        {
            int name = 0;
            bool suc = false;
            using (var access = new ModelContainer())
            {
                try
                {
                    #region Validation
                    if (MenadzerMD.Ime == null)
                    {
                        MessageBox.Show("Polje ime mora biti popunjeno!");
                        return;
                    }
                    else if (MenadzerMD.Prezime == null)
                    {
                        MessageBox.Show("Polje prezime mora biti popunjeno!");
                        return;
                    }
                    else if (MenadzerMD.Staz < 0)
                    {
                        MessageBox.Show("Polje Staz mora biti popunjeno!");
                        return;
                    }
                    else
                    {
                        try
                        {
                            name = Int32.Parse(MenadzerMD.Ime);
                            suc = true;
                            if (suc)
                            {
                                MessageBox.Show("Polje ime ne smije biti popunjeno brojevima!");
                                suc = false;
                                return;
                            }

                            try
                            {
                                name = Int32.Parse(MenadzerMD.Prezime);
                                suc = true;
                                if (suc)
                                {
                                    MessageBox.Show("Polje prezime ne smije biti popunjeno brojevima!");
                                    suc = false;
                                    return;
                                }

                                if (MenadzerMD.Staz < 0)
                                {
                                    MessageBox.Show("Polje Staz ne smije biti manje od 0!");
                                    return;

                                }

                            }
                            catch
                            {
                                if (MenadzerMD.Staz < 0)
                                {
                                    MessageBox.Show("Polje staz ne smije biti manje od 0!");
                                    return;

                                }
                            }

                        }
                        catch
                        {
                            try
                            {
                                name = Int32.Parse(MenadzerMD.Prezime);
                                suc = true;
                                if (suc)
                                {
                                    MessageBox.Show("Polje prezime ne smije biti popunjeno brojevima!");
                                    suc = false;
                                    return;
                                }
                                if (MenadzerMD.Staz < 0)
                                {
                                    MessageBox.Show("Polje Staz ne smije biti manje od 0!");
                                    return;

                                }

                            }
                            catch
                            {
                                if (MenadzerMD.Staz < 0)
                                {
                                    MessageBox.Show("Polje Staz karata ne smije biti manje od 0!");
                                    return;

                                }
                            }
                        }
                    }
                    #endregion
                    access.Radniks_Menadzer.Add(MenadzerMD);
                    
                    int success = access.SaveChanges();
                    if (success > 0)
                    {
                        MessageBox.Show("Uspijesno dodat objekat!");
                    }

                    MenadzerMD = new Menadzer();
                    RadnikMD = new Radnik();
                    Menadzeri = GetAll();
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
                    if (MenadzerMD.Ime == null)
                    {
                        MessageBox.Show("Polje ime mora biti popunjeno!");
                        return;
                    }
                    else if (MenadzerMD.Prezime == null)
                    {
                        MessageBox.Show("Polje prezime mora biti popunjeno!");
                        return;
                    }
                    else if (MenadzerMD.Staz < 0)
                    {
                        MessageBox.Show("Polje Staz mora biti popunjeno!");
                        return;
                    }
                    else
                    {
                        try
                        {
                            name = Int32.Parse(MenadzerMD.Ime);
                            suc = true;
                            if (suc)
                            {
                                MessageBox.Show("Polje ime ne smije biti popunjeno brojevima!");
                                suc = false;
                                return;
                            }

                            try
                            {
                                name = Int32.Parse(MenadzerMD.Prezime);
                                suc = true;
                                if (suc)
                                {
                                    MessageBox.Show("Polje prezime ne smije biti popunjeno brojevima!");
                                    suc = false;
                                    return;
                                }

                                if (MenadzerMD.Staz < 0)
                                {
                                    MessageBox.Show("Polje Staz ne smije biti manje od 0!");
                                    return;

                                }

                            }
                            catch
                            {
                                if (MenadzerMD.Staz < 0)
                                {
                                    MessageBox.Show("Polje staz ne smije biti manje od 0!");
                                    return;

                                }
                            }

                        }
                        catch
                        {
                            try
                            {
                                name = Int32.Parse(MenadzerMD.Prezime);
                                suc = true;
                                if (suc)
                                {
                                    MessageBox.Show("Polje prezime ne smije biti popunjeno brojevima!");
                                    suc = false;
                                    return;
                                }
                                if (MenadzerMD.Staz < 0)
                                {
                                    MessageBox.Show("Polje Staz ne smije biti manje od 0!");
                                    return;

                                }

                            }
                            catch
                            {
                                if (MenadzerMD.Staz < 0)
                                {
                                    MessageBox.Show("Polje Staz karata ne smije biti manje od 0!");
                                    return;

                                }
                            }
                        }
                    }
                    #endregion
                    access.Radniks_Menadzer.FirstOrDefault(n => n.IdRadnika == SelektovaniMenadzer.IdRadnika).Staz = MenadzerMD.Staz;
                    access.Radniks_Menadzer.FirstOrDefault(n => n.IdRadnika == SelektovaniMenadzer.IdRadnika).Ime = RadnikMD.Ime;
                    access.Radniks_Menadzer.FirstOrDefault(n => n.IdRadnika == SelektovaniMenadzer.IdRadnika).Prezime = RadnikMD.Prezime;

                    int success = access.SaveChanges();
                    if (success > 0)
                    {
                        MessageBox.Show("Uspesno modifikovan objekat!");
                    }

                    Menadzeri = GetAll();

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
                    access.Radniks.Remove(access.Radniks.FirstOrDefault(n => n.IdRadnika == SelektovaniMenadzer.IdRadnika));
                    access.Radniks_Menadzer.Remove(access.Radniks_Menadzer.FirstOrDefault(n => n.IdRadnika == SelektovaniMenadzer.IdRadnika));

                    int success = access.SaveChanges();
                    if (success > 0)
                    {
                        MessageBox.Show("Uspijesno obrisan objekat!");
                    }

                    Menadzeri = GetAll();
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
                    MenadzerMD = new Menadzer();
                    RadnikMD = new Radnik();
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
                    MenadzerMD = SelektovaniMenadzer;
                    if (MenadzerMD != null)
                    {
                        RadnikMD = GetAllRadnici().Where(n => n.IdRadnika == MenadzerMD.IdRadnika).FirstOrDefault();
                    }
                    else
                    {
                        RadnikMD = new Radnik();
                    }

                    break;
            }
        }
       
        #region Property
        public Menadzer MenadzerMD
        {
            get { return menadzerMD; }
            set
            {

                if (value != menadzerMD)
                {
                    menadzerMD = value;
                    OnPropertyChanged("MenadzerMD");
                }
            }
        }
        public Radnik RadnikMD
        {
            get { return radnikMD; }
            set
            {

                if (value != radnikMD)
                {
                    radnikMD = value;
                    OnPropertyChanged("RadnikMD");
                }
            }
        }
        public Menadzer SelektovaniMenadzer
        {
            get { return selektovaniMenadzer; }
            set
            {

                if (value != selektovaniMenadzer)
                {
                    selektovaniMenadzer = value;
                    OnPropertyChanged("SelektovaniMenadzer");
                    if (IsVisibleModifikuj == true)
                    {
                        MenadzerMD = SelektovaniMenadzer;
                        if (MenadzerMD != null)
                        {
                            RadnikMD = GetAllRadnici().Where(n => n.IdRadnika == MenadzerMD.IdRadnika).FirstOrDefault();
                        }
                        else
                        {
                            RadnikMD = new Radnik();
                        }
                    }
                }
            }
        }
        public BindingList<Menadzer> Menadzeri
        {
            get { return menadzeri; }
            set
            {

                if (value != menadzeri)
                {
                    menadzeri = value;
                    OnPropertyChanged("Menadzeri");
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
        #endregion

    }
}
