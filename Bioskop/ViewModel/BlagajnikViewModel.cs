using Bioskop.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Bioskop.ViewModel
{
    public class BlagajnikViewModel : BindableBase
    {
        private bool isVisibleModifikuj;
        private bool isVisibleObrisi;
        private bool isVisibleDodaj;
        private bool isVisibleStek;

        private Blagajnik blagajnikMD;
        private Radnik radnikMD;
        private Blagajnik selektovaniBlagajnik;
        public BindingList<Blagajnik> blagajnici;

        public ICommand NavCommand { get; private set; }
        public ICommand DodajCommand { get; private set; }
        public ICommand ModifikujCommand { get; private set; }
        public ICommand ObrisiCommand { get; private set; }
      

        public BlagajnikViewModel()
        {
            IsVisibleModifikuj = false;
            IsVisibleDodaj = false;
            IsVisibleObrisi = false;
            IsVisibleStek = false;

            NavCommand = new MyICommand<string>(OnNav);
            DodajCommand = new MyICommand(OnDodaj);
            ModifikujCommand = new MyICommand(OnModifikuj);
            ObrisiCommand = new MyICommand(OnObrisi);

            BlagajnikMD = new Blagajnik();
            SelektovaniBlagajnik = new Blagajnik();
            RadnikMD = new Radnik();
            Blagajnici = GetAll();
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
        public BindingList<Blagajnik> GetAll()
        {
            using (var access = new ModelContainer())
            {
                var vl = access.Radniks_Blagajnik;
                
                BindingList<Blagajnik> vs = new BindingList<Blagajnik>();

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
                    if (BlagajnikMD.Ime == null)
                    {
                        MessageBox.Show("Polje ime mora biti popunjeno!");
                        return;
                    }
                    else if (BlagajnikMD.Prezime == null)
                    {
                        MessageBox.Show("Polje prezime mora biti popunjeno!");
                        return;
                    }
                    else if (BlagajnikMD.BrojProdatihKarata < 0)
                    {
                        MessageBox.Show("Polje Broj prodatih karata mora biti popunjeno!");
                        return;
                    }
                    else
                    {
                        try
                        {
                            name = Int32.Parse(BlagajnikMD.Ime);
                            suc = true;
                            if (suc)
                            {
                                MessageBox.Show("Polje ime ne smije biti popunjeno brojevima!");
                                suc = false;
                                return;
                            }

                            try
                            {
                                name = Int32.Parse(BlagajnikMD.Prezime);
                                suc = true;
                                if (suc)
                                {
                                    MessageBox.Show("Polje prezime ne smije biti popunjeno brojevima!");
                                    suc = false;
                                    return;
                                }

                                if (BlagajnikMD.BrojProdatihKarata < 0)
                                {
                                    MessageBox.Show("Polje Brpj prodatih karata ne smije biti manje od 0!");
                                    return;

                                }

                            }
                            catch
                            {
                                if (BlagajnikMD.BrojProdatihKarata < 0)
                                {
                                    MessageBox.Show("Polje Brpj prodatih karata ne smije biti manje od 0!");
                                    return;

                                }
                            }

                        }
                        catch
                        {
                            try
                            {
                                name = Int32.Parse(BlagajnikMD.Prezime);
                                suc = true;
                                if (suc)
                                {
                                    MessageBox.Show("Polje prezime ne smije biti popunjeno brojevima!");
                                    suc = false;
                                    return;
                                }
                                if (BlagajnikMD.BrojProdatihKarata < 0)
                                {
                                    MessageBox.Show("Polje Brpj prodatih karata ne smije biti manje od 0!");
                                    return;

                                }

                            }
                            catch
                            {
                                if (BlagajnikMD.BrojProdatihKarata < 0)
                                {
                                    MessageBox.Show("Polje Brpj prodatih karata ne smije biti manje od 0!");
                                    return;

                                }
                            }
                        }
                    }
                    #endregion

                    access.Radniks_Blagajnik.Add(BlagajnikMD);
                    int success = access.SaveChanges();
                    if (success > 0)
                    {
                        MessageBox.Show("Uspijesno dodat objekat!");
                    }

                    BlagajnikMD = new Blagajnik();
                    RadnikMD = new Radnik();
                    Blagajnici = GetAll();
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
                    if (BlagajnikMD.Ime == null)
                    {
                        MessageBox.Show("Polje ime mora biti popunjeno!");
                        return;
                    }
                    else if (BlagajnikMD.Prezime == null)
                    {
                        MessageBox.Show("Polje prezime mora biti popunjeno!");
                        return;
                    }
                    else if (BlagajnikMD.BrojProdatihKarata < 0)
                    {
                        MessageBox.Show("Polje Broj prodatih karata mora biti popunjeno!");
                        return;
                    }
                    else
                    {
                        try
                        {
                            name = Int32.Parse(BlagajnikMD.Ime);
                            suc = true;
                            if (suc)
                            {
                                MessageBox.Show("Polje ime ne smije biti popunjeno brojevima!");
                                suc = false;
                                return;
                            }

                            try
                            {
                                name = Int32.Parse(BlagajnikMD.Prezime);
                                suc = true;
                                if (suc)
                                {
                                    MessageBox.Show("Polje prezime ne smije biti popunjeno brojevima!");
                                    suc = false;
                                    return;
                                }

                                if (BlagajnikMD.BrojProdatihKarata < 0)
                                {
                                    MessageBox.Show("Polje Brpj prodatih karata ne smije biti manje od 0!");
                                    return;

                                }

                            }
                            catch
                            {
                                if (BlagajnikMD.BrojProdatihKarata < 0)
                                {
                                    MessageBox.Show("Polje Brpj prodatih karata ne smije biti manje od 0!");
                                    return;

                                }
                            }

                        }
                        catch
                        {
                            try
                            {
                                name = Int32.Parse(BlagajnikMD.Prezime);
                                suc = true;
                                if (suc)
                                {
                                    MessageBox.Show("Polje prezime ne smije biti popunjeno brojevima!");
                                    suc = false;
                                    return;
                                }
                                if (BlagajnikMD.BrojProdatihKarata < 0)
                                {
                                    MessageBox.Show("Polje Brpj prodatih karata ne smije biti manje od 0!");
                                    return;

                                }

                            }
                            catch
                            {
                                if (BlagajnikMD.BrojProdatihKarata < 0)
                                {
                                    MessageBox.Show("Polje Brpj prodatih karata ne smije biti manje od 0!");
                                    return;

                                }
                            }
                        }
                    }
                    #endregion

                    access.Radniks_Blagajnik.FirstOrDefault(n => n.IdRadnika == SelektovaniBlagajnik.IdRadnika).BrojProdatihKarata = BlagajnikMD.BrojProdatihKarata;
                    access.Radniks_Blagajnik.FirstOrDefault(n => n.IdRadnika == SelektovaniBlagajnik.IdRadnika).Ime = BlagajnikMD.Ime;
                    access.Radniks_Blagajnik.FirstOrDefault(n => n.IdRadnika == SelektovaniBlagajnik.IdRadnika).Prezime = BlagajnikMD.Prezime;


                    int success = access.SaveChanges();
                    if (success > 0)
                    {
                        MessageBox.Show("Uspijesno modifikovan objekat!");
                    }

                    Blagajnici = GetAll();

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
                    var blagajnik = access.Radniks_Blagajnik.FirstOrDefault(n => n.IdRadnika == SelektovaniBlagajnik.IdRadnika);
                    //access.Usluzujes.Remove(access.Usluzujes.FirstOrDefault(n => n.BlagajnikIdRadnika == SelektovaniBlagajnik.IdRadnika));
                    access.Radniks_Blagajnik.Remove(blagajnik);

                    access.Radniks.Remove(access.Radniks.FirstOrDefault(n => n.IdRadnika == SelektovaniBlagajnik.IdRadnika));
                    
                    int success = access.SaveChanges();
                    if (success > 0)
                    {
                        MessageBox.Show("Uspesno obrisan objekat!");
                    }

                    Blagajnici = GetAll();
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
                    BlagajnikMD = new Blagajnik();
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
                    BlagajnikMD = SelektovaniBlagajnik;
                    if (BlagajnikMD != null)
                    {
                        RadnikMD = GetAllRadnici().Where(n => n.IdRadnika == BlagajnikMD.IdRadnika).FirstOrDefault();
                    }
                    else
                    {
                        RadnikMD = new Radnik();
                    }

                    break;
            }
        }

        
        #region Property
        public Blagajnik BlagajnikMD
        {
            get { return blagajnikMD; }
            set
            {

                if (value != blagajnikMD)
                {
                    blagajnikMD = value;
                    OnPropertyChanged("BlagajnikMD");
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
        public Blagajnik SelektovaniBlagajnik
        {
            get { return selektovaniBlagajnik; }
            set
            {

                if (value != selektovaniBlagajnik)
                {
                    selektovaniBlagajnik = value;
                    OnPropertyChanged("SelektovaniBlagajnik");
                    if (IsVisibleModifikuj == true)
                    {
                        BlagajnikMD = SelektovaniBlagajnik;
                        if (BlagajnikMD != null)
                        {
                            RadnikMD = GetAllRadnici().Where(n => n.IdRadnika == BlagajnikMD.IdRadnika).FirstOrDefault();
                        }
                        else
                        {
                            RadnikMD = new Radnik();
                        }
                    }
                }
            }
        }
        public BindingList<Blagajnik> Blagajnici
        {
            get { return blagajnici; }
            set
            {

                if (value != blagajnici)
                {
                    blagajnici = value;
                    OnPropertyChanged("Blagajnici");
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
