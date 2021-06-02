using Bioskop.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bioskop
{
    public class MainWindowViewModel : BindableBase
    {
        public MyICommand<string> NavCommand { get; private set; }

        private BlagajnikViewModel blagajnikViewModel = new BlagajnikViewModel();
        private CistacicaViewModel cistacicaViewModel = new CistacicaViewModel();
        private FilmViewModel filmViewModel = new FilmViewModel();
        private GlumacViewModel glumacViewModel = new GlumacViewModel();
        private KartaViewModel kartaViewModel = new KartaViewModel();
        private MenadzerViewModel menadzerViewModel = new MenadzerViewModel();
        private RepertoarViewModel repertoarViewModel = new RepertoarViewModel();
        private SalaViewModel salaViewModel = new SalaViewModel();
        private SjedisteViewModel sjedisteViewModel = new SjedisteViewModel();
        private PosjetilacViewModel posjetilacViewModel = new PosjetilacViewModel();
        private PrikazujeViewModel prikazujeViewModel = new PrikazujeViewModel();

        private BindableBase currentViewModel;

        public MainWindowViewModel()
        {
            NavCommand = new MyICommand<string>(OnNav);
            CurrentViewModel = cistacicaViewModel;
        }

        public BindableBase CurrentViewModel
        {
            get { return currentViewModel; }
            set
            {
                SetProperty(ref currentViewModel, value);
            }
        }

        private void OnNav(string destination)
        {
            switch (destination)
            {
                case "blagajnik":
                    CurrentViewModel = blagajnikViewModel;
                    break;
                case "cistacica":
                    CurrentViewModel = cistacicaViewModel;
                    break;
                case "film":
                    CurrentViewModel = filmViewModel;
                    break;
                case "glumac":
                    CurrentViewModel = glumacViewModel;
                    break;
                case "karta":
                    CurrentViewModel = kartaViewModel;
                    break;
                case "menadzer":
                    CurrentViewModel = menadzerViewModel;
                    break;
                case "repertoar":
                    CurrentViewModel = repertoarViewModel;
                    break;
                case "sala":
                    CurrentViewModel = salaViewModel;
                    break;
                case "sjediste":
                    CurrentViewModel = sjedisteViewModel;
                    break;
                case "posjetilac":
                    CurrentViewModel = posjetilacViewModel;
                    break;
                case "prikazuje":
                    CurrentViewModel = prikazujeViewModel;
                    break;
            }
        }

    }
}
