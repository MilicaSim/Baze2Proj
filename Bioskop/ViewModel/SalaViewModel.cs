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
    public class SalaViewModel : BindableBase
    {
        private bool isVisibleModifikuj;
        private bool isVisibleObrisi;
        private bool isVisibleDodaj;
        private bool isVisibleStek;

        private Sala salaMD;
        private Sala selektovanaSala;
        public BindingList<Sala> sale;

        private ObservableCollection<Sjediste> sjedistaList = new ObservableCollection<Sjediste>();
        private Sjediste currentSjediste;
        private Dictionary<int, bool> sjedistaBool = new Dictionary<int, bool>();
        private ICommand visibilityCheckedCommand;
        private ICommand visibilityUncheckedCommand;

        public ICommand NavCommand { get; private set; }
        public ICommand DodajCommand { get; private set; }
        public ICommand ModifikujCommand { get; private set; }
        public ICommand ObrisiCommand { get; private set; }


        public SalaViewModel()
        {
            IsVisibleModifikuj = false;
            IsVisibleDodaj = false;
            IsVisibleObrisi = false;
            IsVisibleStek = false;

            NavCommand = new MyICommand<string>(OnNav);
            DodajCommand = new MyICommand(OnDodaj);
            ModifikujCommand = new MyICommand(OnModifikuj);
            ObrisiCommand = new MyICommand(OnObrisi);

            SalaMD = new Sala();
            SelektovanaSala = new Sala();
            Sale = GetAll();
            SjedistaList = GetSjedista();
        }

        public BindingList<Sala> GetAll()
        {
            using (var access = new ModelContainer())
            {

                var vl = access.Salas;
                BindingList<Sala> vs = new BindingList<Sala>();
                foreach (var v in vl)
                {
                    vs.Add(v);

                }
                return vs;
            }
        }
        public ObservableCollection<Sjediste> GetSjedista()
        {
            using (var access = new ModelContainer())
            {

                var vl = access.Sjedistes;
                ObservableCollection<Sjediste> vs = new ObservableCollection<Sjediste>();
                foreach (var v in vl)
                {
                    if (!v.Zauzeto)
                    {
                        sjedistaBool.Add(v.IdSjedista, false);
                        vs.Add(v);
                    }

                }
                return vs;
            }
        }
        public void SjedistaNaTrue(int idSale)
        {
            using (var access = new ModelContainer())
            {

                var vl = access.Sadrzis;
                foreach (var v in vl)
                {
                    if (v.SalaIdSale == idSale)
                        v.Sjediste.Zauzeto = false;

                }
                int success = access.SaveChanges();
            }
        }


        public void OnDodaj()
        {
            bool successs = true;
            using (var access = new ModelContainer())
            {
                try
                {
                    #region Validation
                    if (SalaMD.Naziv == null)
                    {
                        MessageBox.Show("Polje naziv mora biti popunjeno!");
                        successs = false;
                        
                    }
                    else if (SalaMD.Kapacitet <= 0)
                    {
                        MessageBox.Show("Polje kapacitet mora biti popunjeno!");
                        successs = false;

                    }
                    else if (SalaMD.BrojRedova<=0)
                    {
                        MessageBox.Show("Polje broj redova mora biti popunjeno!");
                        successs = false;

                    }
                    else
                    {
                        foreach (var sala in Sale)
                        {
                            if (sala.Naziv.Equals(SalaMD.Naziv))
                            {
                                MessageBox.Show("Sala sa tim imenom vec postoji!");
                                successs = false;
                                break;
                            }
                        }
                    }
                    
                    #endregion

                    if (successs)
                    {
                        access.Salas.Add(SalaMD);

                        int success = access.SaveChanges();
                        if (success > 0)
                        {
                            MessageBox.Show("Uspijesno dodat objekat!");
                        }

                        foreach (var item in SjedistaBool)
                        {
                            if (item.Value)
                            {
                                var sjediste = access.Sjedistes.FirstOrDefault(x => x.IdSjedista == item.Key);
                                sjediste.Zauzeto = true;
                                success = access.SaveChanges();
                                var sala = access.Salas.FirstOrDefault(x => x.Naziv == SalaMD.Naziv);
                                Sadrzi sazdrzi = new Sadrzi();
                                sazdrzi.SalaIdSale = sala.IdSale;
                                sazdrzi.SjedisteIdSjedista = sjediste.IdSjedista;
                                access.Sadrzis.Add(sazdrzi);
                                success = access.SaveChanges();
                            }
                        }



                        SalaMD = new Sala();
                        Sale = GetAll();
                    }

                }
                catch (DbUpdateException)
                {
                    MessageBox.Show("Sifra radnika vec postoji!");
                }

            }
        }
        public void OnModifikuj()
        {
            bool successs = true;
            using (var access = new ModelContainer())
            {
                try
                {
                    #region Validation
                    if (SalaMD.Naziv == null)
                    {
                        MessageBox.Show("Polje naziv mora biti popunjeno!");
                        successs = false;

                    }
                    else if (SalaMD.Kapacitet <= 0)
                    {
                        MessageBox.Show("Polje kapacitet mora biti popunjeno!");
                        successs = false;

                    }
                    else if (SalaMD.BrojRedova <= 0)
                    {
                        MessageBox.Show("Polje broj redova mora biti popunjeno!");
                        successs = false;

                    }
                    else
                    {
                        foreach (var sala in Sale)
                        {
                            if (sala.Naziv.Contains(SalaMD.Naziv))
                            {
                                MessageBox.Show("Sala sa tim imenom vec postoji!");
                                successs = false;
                                break;
                            }
                        }
                    }

                    #endregion

                    if (successs)
                    {
                        access.Salas.Where(n => n.IdSale == SelektovanaSala.IdSale).FirstOrDefault().Kapacitet = SalaMD.Kapacitet;
                        access.Salas.Where(n => n.IdSale == SelektovanaSala.IdSale).FirstOrDefault().Naziv = SalaMD.Naziv;
                        access.Salas.Where(n => n.IdSale == SelektovanaSala.IdSale).FirstOrDefault().BrojRedova = SalaMD.BrojRedova;

                        int success = access.SaveChanges();
                        if (success > 0)
                        {
                            MessageBox.Show("Uspijesno modifikovan objekat!");
                        }

                        Sale = GetAll();
                    }

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
                    var sala = access.Salas.FirstOrDefault(n => n.IdSale == SelektovanaSala.IdSale);
                    sala.Cistacicas.Clear();
                    sala.Sadrzis.Clear();
                    SjedistaNaTrue(sala.IdSale);

                    var prikazivanje = access.Prikazujes.FirstOrDefault(x => x.SadrziSalaIdSale == sala.IdSale);

                    if (prikazivanje != null)
                    {
                        var karta = access.Kartas.FirstOrDefault(x => x.PrikazujeIdPrikazivanja == prikazivanje.IdPrikazivanja);
                        if (karta != null)
                        {
                            access.Kartas.Remove(karta);
                            access.Prikazujes.Remove(prikazivanje);
                            access.Sadrzis.Remove(access.Sadrzis.FirstOrDefault(n => n.SalaIdSale == SelektovanaSala.IdSale));

                            access.Salas.Remove(sala);

                        }
                        else
                        {
                            access.Prikazujes.Remove(prikazivanje);
                            access.Sadrzis.Remove(access.Sadrzis.FirstOrDefault(n => n.SalaIdSale == SelektovanaSala.IdSale));

                            access.Salas.Remove(sala);
                        }
                    }
                    else
                    {
                        access.Sadrzis.Remove(access.Sadrzis.FirstOrDefault(n => n.SalaIdSale == SelektovanaSala.IdSale));

                        access.Salas.Remove(sala);
                    }
                    
                    int success = access.SaveChanges();
                    if (success > 0)
                    {
                        MessageBox.Show("Uspesno obrisan objekat!");
                    }

                    Sale = GetAll();
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
                    SalaMD = new Sala();
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
                    SalaMD = SelektovanaSala;
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
        public Sala SalaMD
        {
            get { return salaMD; }
            set
            {

                if (value != salaMD)
                {
                    salaMD = value;
                    OnPropertyChanged("SalaMD");
                }
            }
        }
        public Sala SelektovanaSala
        {
            get { return selektovanaSala; }
            set
            {

                if (value != selektovanaSala)
                {
                    selektovanaSala = value;
                    OnPropertyChanged("SelektovaniGlumac");
                    if (IsVisibleModifikuj == true)
                    {
                        SalaMD = SelektovanaSala;
                       
                    }
                }
            }
        }
        public BindingList<Sala> Sale
        {
            get { return sale; }
            set
            {

                if (value != sale)
                {
                    sale = value;
                    OnPropertyChanged("Sale");
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

        public ObservableCollection<Sjediste> SjedistaList
        {
            get => sjedistaList;
            set
            {
                if (sjedistaList == value)
                    return;
                sjedistaList = value;
                OnPropertyChanged("SjedistaList");
            }
        }

        public Sjediste CurrentSjediste
        {
            get => currentSjediste;
            set
            {
                if (currentSjediste == value)
                    return;
                currentSjediste = value;
                OnPropertyChanged("CurrentSjediste");
            }
        }

        public Dictionary<int, bool> SjedistaBool { get => sjedistaBool; set => sjedistaBool = value; }

        public ICommand VisibilityCheckedCommand => visibilityCheckedCommand ?? (visibilityCheckedCommand = new MyICommand<KeyValuePair<int, bool>>(VisibilityCheckedCommandExecute));

        public ICommand VisibilityUncheckedCommand => visibilityUncheckedCommand ?? (visibilityUncheckedCommand = new MyICommand<KeyValuePair<int, bool>>(VisibilityUncheckedCommandExecute));

        private void VisibilityCheckedCommandExecute(KeyValuePair<int, bool> name)
        {
            SjedistaBool[name.Key] = true;
            OnPropertyChanged("SjedistaBool");
        }
        private void VisibilityUncheckedCommandExecute(KeyValuePair<int, bool> name)
        {
            SjedistaBool[name.Key] = false;
            OnPropertyChanged("SjedistaBool");
        }

        #endregion

    }
}
