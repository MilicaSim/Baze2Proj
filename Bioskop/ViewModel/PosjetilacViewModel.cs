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
    public class PosjetilacViewModel : BindableBase
    {
        private bool isVisibleModifikuj;
        private bool isVisibleObrisi;
        private bool isVisibleDodaj;
        private bool isVisibleStek;

        private Posjetilac posjetilacMD;
        private Posjetilac selektovaniPosjetilac;
        public BindingList<Posjetilac> posjetioci;
        private BindingList<int> blagajnici;
        private int posjetilacBlagajnik;
        private Blagajnik _selectedBlagajnik;
        private ObservableCollection<Blagajnik> _listBlagajnika = new ObservableCollection<Blagajnik>();

        public ICommand NavCommand { get; private set; }
        public ICommand DodajCommand { get; private set; }
        public ICommand ModifikujCommand { get; private set; }
        public ICommand ObrisiCommand { get; private set; }


        public PosjetilacViewModel()
        {
            IsVisibleModifikuj = false;
            IsVisibleDodaj = false;
            IsVisibleObrisi = false;
            IsVisibleStek = false;

            NavCommand = new MyICommand<string>(OnNav);
            DodajCommand = new MyICommand(OnDodaj);
            ModifikujCommand = new MyICommand(OnModifikuj);
            ObrisiCommand = new MyICommand(OnObrisi);

            PosjetilacMD = new Posjetilac();
            SelektovaniPosjetilac = new Posjetilac();
            Posjetioci = GetAll();
            Blagajnici = GetAllBlagajnici();
        }

        public BindingList<int> GetAllBlagajnici()
        {
            _listBlagajnika.Clear();

            using (var access = new ModelContainer())
            {
                var vl = access.Radniks_Blagajnik;

                BindingList<int> vs = new BindingList<int>();
                foreach (var v in vl)
                {
                    _listBlagajnika.Add(v);
                    vs.Add(v.IdRadnika);
                }
                return vs;
            }
        }

        public BindingList<Posjetilac> GetAll()
        {
            using (var access = new ModelContainer())
            {
                var vl = access.Posjetilacs;
                BindingList<Posjetilac> vs = new BindingList<Posjetilac>();
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
                    if (PosjetilacMD.Ime == null)
                    {
                        MessageBox.Show("Polje ime mora biti popunjeno!");
                        return;
                    }
                    else if (PosjetilacMD.Prezime == null)
                    {
                        MessageBox.Show("Polje prezime mora biti popunjeno!");
                        return;
                    }
                    else
                    {
                        try
                        {
                            name = Int32.Parse(PosjetilacMD.Ime);
                            suc = true;
                            if (suc)
                            {
                                MessageBox.Show("Polje ime ne smije biti popunjeno brojevima!");
                                suc = false;
                                return;
                            }

                            try
                            {
                                name = Int32.Parse(PosjetilacMD.Prezime);
                                suc = true;
                                if (suc)
                                {
                                    MessageBox.Show("Polje prezime ne smije biti popunjeno brojevima!");
                                    suc = false;
                                    return;
                                }
                                

                            }
                            catch
                            {
                            }

                        }
                        catch
                        {
                            try
                            {
                                name = Int32.Parse(PosjetilacMD.Prezime);
                                suc = true;
                                if (suc)
                                {
                                    MessageBox.Show("Polje prezime ne smije biti popunjeno brojevima!");
                                    suc = false;
                                    return;
                                }

                            }
                            catch
                            {
                            }
                        }
                    }
                    #endregion
                    PosjetilacMD.Blagajnik = SelectedBlagajnik;

                    access.Posjetilacs.Add(PosjetilacMD);


                    int success = access.SaveChanges();
                    if (success > 0)
                    {
                        MessageBox.Show("Uspijesno dodat objekat!");
                    }

                    PosjetilacMD = new Posjetilac();
                    Posjetioci = GetAll();
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
                    if (PosjetilacMD.Ime == null)
                    {
                        MessageBox.Show("Polje ime mora biti popunjeno!");
                        return;
                    }
                    else if (PosjetilacMD.Prezime == null)
                    {
                        MessageBox.Show("Polje prezime mora biti popunjeno!");
                        return;
                    }
                    else
                    {
                        try
                        {
                            name = Int32.Parse(PosjetilacMD.Ime);
                            suc = true;
                            if (suc)
                            {
                                MessageBox.Show("Polje ime ne smije biti popunjeno brojevima!");
                                suc = false;
                                return;
                            }

                            try
                            {
                                name = Int32.Parse(PosjetilacMD.Prezime);
                                suc = true;
                                if (suc)
                                {
                                    MessageBox.Show("Polje prezime ne smije biti popunjeno brojevima!");
                                    suc = false;
                                    return;
                                }


                            }
                            catch
                            {
                            }

                        }
                        catch
                        {
                            try
                            {
                                name = Int32.Parse(PosjetilacMD.Prezime);
                                suc = true;
                                if (suc)
                                {
                                    MessageBox.Show("Polje prezime ne smije biti popunjeno brojevima!");
                                    suc = false;
                                    return;
                                }

                            }
                            catch
                            {
                            }
                        }
                    }
                    #endregion
                    access.Posjetilacs.FirstOrDefault(n => n.IdPosjetioca == SelektovaniPosjetilac.IdPosjetioca).Ime = PosjetilacMD.Ime;
                    access.Posjetilacs.FirstOrDefault(n => n.IdPosjetioca == SelektovaniPosjetilac.IdPosjetioca).Prezime = PosjetilacMD.Prezime;
                    //blagajnici

                    int success = access.SaveChanges();
                    if (success > 0)
                    {
                        MessageBox.Show("Uspesno modifikovan objekat!");
                    }

                    Posjetioci = GetAll();

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
                    var posjetilac = access.Posjetilacs.FirstOrDefault(n => n.IdPosjetioca == SelektovaniPosjetilac.IdPosjetioca);
                    posjetilac.Kartas.Clear();
                    var karta = access.Kartas.FirstOrDefault(n => n.PosjetilacIdPosjetioca == posjetilac.IdPosjetioca);
                    if (karta != null)
                    {
                        access.Kartas.Remove(karta);
                        access.Posjetilacs.Remove(posjetilac);
                    }
                    else
                    {
                        access.Posjetilacs.Remove(posjetilac);
                    }


                    int success = access.SaveChanges();
                    if (success > 0)
                    {
                        MessageBox.Show("Uspesno obrisan objekat!");
                    }

                    Posjetioci = GetAll();
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
                    PosjetilacMD = new Posjetilac();
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
                    PosjetilacMD = SelektovaniPosjetilac;

                    break;
            }
        }
        
        #region Property
        public Posjetilac PosjetilacMD
        {
            get { return posjetilacMD; }
            set
            {

                if (value != posjetilacMD)
                {
                    posjetilacMD = value;
                    OnPropertyChanged("PosjetilacMD");
                }
            }
        }
       
        public Posjetilac SelektovaniPosjetilac
        {
            get { return selektovaniPosjetilac; }
            set
            {

                if (value != selektovaniPosjetilac)
                {
                    selektovaniPosjetilac = value;
                    OnPropertyChanged("SelektovaniPosjetilac");
                }
            }
        }
        public BindingList<Posjetilac> Posjetioci
        {
            get { return posjetioci; }
            set
            {

                if (value != posjetioci)
                {
                    posjetioci = value;
                    OnPropertyChanged("Posjetioci");
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

        public BindingList<int> Blagajnici { get => blagajnici; set => blagajnici = value; }
        public int PosjetilacBlagajnik
        {
            get { return posjetilacBlagajnik; }
            set
            {

                if (value != posjetilacBlagajnik)
                {
                    posjetilacBlagajnik = value;
                    OnPropertyChanged("PosjetilacBlagajnik");
                }
            }
        }

        public Blagajnik SelectedBlagajnik { get => _selectedBlagajnik; set => _selectedBlagajnik = value; }
        public ObservableCollection<Blagajnik> ListBlagajnika { get => _listBlagajnika; set => _listBlagajnika = value; }
        #endregion

    }
}
