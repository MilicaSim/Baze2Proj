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
using System.Collections.ObjectModel;

namespace Bioskop.ViewModel
{
    public class CistacicaViewModel : BindableBase
    {
        private bool isVisibleModifikuj;
        private bool isVisibleObrisi;
        private bool isVisibleDodaj;
        private bool isVisibleStek;

        private ObservableCollection<Sala> saleList = new ObservableCollection<Sala>();
        private Sala currentSale;
        private Dictionary<string, bool> saleBool = new Dictionary<string, bool>();
        private ICommand visibilityCheckedCommand;
        private ICommand visibilityUncheckedCommand;

        private Cistacica cistacicaMD;
        private Radnik radnikMD;
        private Cistacica selektovanaCistacica;
        public BindingList<Cistacica> cistacice;

        public ICommand NavCommand { get; private set; }
        public ICommand DodajCommand { get; private set; }
        public ICommand ModifikujCommand { get; private set; }
        public ICommand ObrisiCommand { get; private set; }


        public CistacicaViewModel()
        {
            IsVisibleModifikuj = false;
            IsVisibleDodaj = false;
            IsVisibleObrisi = false;
            IsVisibleStek = false;

            NavCommand = new MyICommand<string>(OnNav);
            DodajCommand = new MyICommand(OnDodaj);
            ModifikujCommand = new MyICommand(OnModifikuj);
            ObrisiCommand = new MyICommand(OnObrisi);

            CistacicaMD = new Cistacica();
            SelektovanaCistacica = new Cistacica();
            RadnikMD = new Radnik();
            Cistacice = GetAll();
            SaleList = GetSale();

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
        public BindingList<Cistacica> GetAll()
        {
            using (var access = new ModelContainer())
            {
                var vl = access.Radniks_Cistacica;

                BindingList<Cistacica> vs = new BindingList<Cistacica>();


                foreach (var v in vl)
                {
                    vs.Add(v);
                }
                return vs;
            }
        }
        public ObservableCollection<Sala> GetSale()
        {
            using (var access = new ModelContainer())
            {
                var vl = access.Salas;

                ObservableCollection<Sala> vs = new ObservableCollection<Sala>();

                foreach (var v in vl)
                {
                    saleBool.Add(v.Naziv, false);
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
                    foreach (var item in SaleBool)
                    {
                        if (item.Value)
                        {
                            var sala = access.Salas.FirstOrDefault(x => x.Naziv == item.Key);
                            CistacicaMD.Salas.Add(sala);
                            CistacicaMD.BrojOcistenihSala++;
                        }

                    }
                    #region Validation
                    if (CistacicaMD.Ime == null)
                        {
                            MessageBox.Show("Polje ime mora biti popunjeno!");
                            return;
                        }
                        else if (CistacicaMD.Prezime == null)
                        {
                            MessageBox.Show("Polje prezime mora biti popunjeno!");
                            return;
                        }
                        else if (CistacicaMD.BrojOcistenihSala < 0)
                        {
                            MessageBox.Show("Polje Broj ociscenih sala mora biti popunjeno!");
                            return;
                        }
                        else
                        {
                            try
                            {
                                name = Int32.Parse(CistacicaMD.Ime);
                                suc = true;
                                if (suc)
                                {
                                    MessageBox.Show("Polje ime ne smije biti popunjeno brojevima!");
                                    suc = false;
                                    return;
                                }

                                try
                                {
                                    name = Int32.Parse(CistacicaMD.Prezime);
                                    suc = true;
                                    if (suc)
                                    {
                                        MessageBox.Show("Polje prezime ne smije biti popunjeno brojevima!");
                                        suc = false;
                                        return;
                                    }

                                    if (CistacicaMD.BrojOcistenihSala < 0)
                                    {
                                        MessageBox.Show("Polje Broj ociscenih sala ne smije biti manje od 0!");
                                        return;

                                    }

                                }
                                catch
                                {
                                    if (CistacicaMD.BrojOcistenihSala < 0)
                                    {
                                        MessageBox.Show("Polje Broj ociscenih sala ne smije biti manje od 0!");
                                        return;

                                    }
                                }

                            }
                            catch
                            {
                                try
                                {
                                    name = Int32.Parse(CistacicaMD.Prezime);
                                    suc = true;
                                    if (suc)
                                    {
                                        MessageBox.Show("Polje prezime ne smije biti popunjeno brojevima!");
                                        suc = false;
                                        return;
                                    }
                                    if (CistacicaMD.BrojOcistenihSala < 0)
                                    {
                                        MessageBox.Show("Polje Broj ociscenih sala ne smije biti manje od 0!");
                                        return;

                                    }

                                }
                                catch
                                {
                                    if (CistacicaMD.BrojOcistenihSala < 0)
                                    {
                                        MessageBox.Show("Polje Broj ociscenih sala ne smije biti manje od 0!");
                                        return;

                                    }
                                }
                            }

                        }

#endregion

                    access.Radniks_Cistacica.Add(CistacicaMD);

                    int success = access.SaveChanges();

                    if (success > 0)
                    {
                        MessageBox.Show("Uspijesno dodat objekat!");
                    }

                    CistacicaMD = new Cistacica();
                    RadnikMD = new Radnik();
                    Cistacice = GetAll();
                }
                catch (DbUpdateException e)
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
                    foreach (var item in SaleBool)
                    {
                        if (item.Value)
                        {
                            var sala = access.Salas.FirstOrDefault(x => x.Naziv == item.Key);
                            CistacicaMD.Salas.Add(sala);
                            CistacicaMD.BrojOcistenihSala++;
                        }
                    }
                    #region Validation
                    if (CistacicaMD.Ime == null)
                    {
                        MessageBox.Show("Polje ime mora biti popunjeno!");
                        return;
                    }
                    else if (CistacicaMD.Prezime == null)
                    {
                        MessageBox.Show("Polje prezime mora biti popunjeno!");
                        return;
                    }
                    else if (CistacicaMD.BrojOcistenihSala < 0)
                    {
                        MessageBox.Show("Polje Broj Ocistenih Sala mora biti popunjeno!");
                        return;
                    }
                    else
                    {
                        try
                        {
                            name = Int32.Parse(CistacicaMD.Ime);
                            suc = true;
                            if (suc)
                            {
                                MessageBox.Show("Polje ime ne smije biti popunjeno brojevima!");
                                suc = false;
                                return;
                            }

                            try
                            {
                                name = Int32.Parse(CistacicaMD.Prezime);
                                suc = true;
                                if (suc)
                                {
                                    MessageBox.Show("Polje prezime ne smije biti popunjeno brojevima!");
                                    suc = false;
                                    return;
                                }

                                if (CistacicaMD.BrojOcistenihSala < 0)
                                {
                                    MessageBox.Show("Polje Broj Ocistenih Sala ne smije biti manje od 0!");
                                    return;

                                }

                            }
                            catch
                            {
                                if (cistacicaMD.BrojOcistenihSala < 0)
                                {
                                    MessageBox.Show("Polje Broj Ocistenih Salane smije biti manje od 0!");
                                    return;

                                }
                            }

                        }
                        catch
                        {
                            try
                            {
                                name = Int32.Parse(cistacicaMD.Prezime);
                                suc = true;
                                if (suc)
                                {
                                    MessageBox.Show("Polje prezime ne smije biti popunjeno brojevima!");
                                    suc = false;
                                    return;
                                }
                                if (cistacicaMD.BrojOcistenihSala < 0)
                                {
                                    MessageBox.Show("Polje Broj ociscenih sala ne smije biti manje od 0!");
                                    return;

                                }

                            }
                            catch
                            {
                                if (cistacicaMD.BrojOcistenihSala < 0)
                                {
                                    MessageBox.Show("Polje Broj ociscenih ne smije biti manje od 0!");
                                    return;

                                }
                               
                            }
                        }
                    }

                    #endregion

                    access.Radniks_Cistacica.FirstOrDefault(n => n.IdRadnika == SelektovanaCistacica.IdRadnika).BrojOcistenihSala = CistacicaMD.BrojOcistenihSala;
                    access.Radniks_Cistacica.FirstOrDefault(n => n.IdRadnika == SelektovanaCistacica.IdRadnika).Ime = CistacicaMD.Ime;
                    access.Radniks_Cistacica.FirstOrDefault(n => n.IdRadnika == SelektovanaCistacica.IdRadnika).Prezime = CistacicaMD.Prezime;


                    int success = access.SaveChanges();
                    if (success > 0)
                    {
                        MessageBox.Show("Uspijesno modifikovan objekat!");
                    }

                    Cistacice = GetAll();

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
                    var cistacica = access.Radniks_Cistacica.FirstOrDefault(n => n.IdRadnika == SelektovanaCistacica.IdRadnika);
                    cistacica.Salas.Clear();
                    access.Radniks_Cistacica.Remove(cistacica);

                    access.Radniks.Remove(access.Radniks.FirstOrDefault(n => n.IdRadnika == SelektovanaCistacica.IdRadnika));

                    int success = access.SaveChanges();
                    if (success > 0)
                    {
                        MessageBox.Show("Uspijesno obrisan objekat!");
                    }

                    Cistacice = GetAll();
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
                    CistacicaMD = new Cistacica();
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
                    CistacicaMD = SelektovanaCistacica;
                    if (CistacicaMD != null)
                    {
                        RadnikMD = GetAllRadnici().Where(n => n.IdRadnika == CistacicaMD.IdRadnika).FirstOrDefault();
                    }
                    else
                    {
                        RadnikMD = new Radnik();
                    }

                    break;
            }
        }



        #region Property
        public Cistacica CistacicaMD
        {
            get { return cistacicaMD; }
            set
            {

                if (value != cistacicaMD)
                {
                    cistacicaMD = value;
                    OnPropertyChanged("CistacicaMD");
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
        public Cistacica SelektovanaCistacica
        {
            get { return selektovanaCistacica; }
            set
            {

                if (value != selektovanaCistacica)
                {
                    selektovanaCistacica = value;
                    OnPropertyChanged("SelektovanaCistacica");
                    if (IsVisibleModifikuj == true)
                    {
                        CistacicaMD = SelektovanaCistacica;
                        if (CistacicaMD != null)
                        {
                            RadnikMD = GetAllRadnici().Where(n => n.IdRadnika == CistacicaMD.IdRadnika).FirstOrDefault();
                        }
                        else
                        {
                            RadnikMD = new Radnik();
                        }
                    }
                }
            }
        }
        public BindingList<Cistacica> Cistacice
        {
            get { return cistacice; }
            set
            {

                if (value != cistacice)
                {
                    cistacice = value;
                    OnPropertyChanged("Cistacice");
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

        public ObservableCollection<Sala> SaleList
        {
            get => saleList;
            set
            {
                if (saleList == value)
                    return;
                saleList = value;
                OnPropertyChanged("SaleList");
            }
        }

        public Sala CurrentSale
        {
            get => currentSale;
            set
            {
                if (currentSale == value)
                    return;
                currentSale = value;
                OnPropertyChanged("CurrentSale");
            }
        }

        public Dictionary<string, bool> SaleBool { get => saleBool; set => saleBool = value; }

        public ICommand VisibilityCheckedCommand => visibilityCheckedCommand ?? (visibilityCheckedCommand = new MyICommand<KeyValuePair<string, bool>>(VisibilityCheckedCommandExecute));

        public ICommand VisibilityUncheckedCommand => visibilityUncheckedCommand ?? (visibilityUncheckedCommand = new MyICommand<KeyValuePair<string, bool>>(VisibilityUncheckedCommandExecute));

        private void VisibilityCheckedCommandExecute(KeyValuePair<string, bool> name)
        {
            SaleBool[name.Key] = true;
            OnPropertyChanged("SaleBool");
        }
        private void VisibilityUncheckedCommandExecute(KeyValuePair<string, bool> name)
        {
            SaleBool[name.Key] = false;
            OnPropertyChanged("SaleBool");
        }
        #endregion

    }
}