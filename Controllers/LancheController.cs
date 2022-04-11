using LanchesMac.Models;
using LanchesMac.Repositories.Interfaces;
using LanchesMac.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace LanchesMac.Controllers
{
    public class LancheController : Controller
    {
        private readonly ILancheRepository _lancheRepository;
        public LancheController(ILancheRepository lancheRepository)
        {
            _lancheRepository = lancheRepository;
        }

        public IActionResult List(string Categoria)
        {

            IEnumerable<Lanche> lanches;
            string categoriaAtual =string.Empty;

            if (string.IsNullOrEmpty(Categoria))
            {

                lanches = _lancheRepository.Lanches.OrderBy(l => l.LancheId);
                categoriaAtual = "Todos os Lanches";
            }
            else
            {
                lanches = _lancheRepository.Lanches.Where(x => x.Categoria.CategoriaNome.Equals(Categoria)).OrderBy(x => x.Nome);
                categoriaAtual = Categoria;
            }

         
            var lanchesListViewModel = new LancheListViewModel
            {
                Lanches = lanches,
                CategoriaAtual = categoriaAtual
            };
            return View(lanchesListViewModel);
        }

        public IActionResult Details(int? lancheId)
        {
            var lanche = _lancheRepository.Lanches.FirstOrDefault(x => x.LancheId == lancheId);
            return View(lanche);
        }
    
        public ViewResult Search(string searchString)
        {
            IEnumerable<Lanche> lanches;
            string categoriaAtual = string.Empty;


            if (string.IsNullOrEmpty(searchString))
            {
                lanches = _lancheRepository.Lanches.OrderBy(x => x.LancheId);

                categoriaAtual = "Todos os lanches";
            }
            else
            {
                lanches = _lancheRepository.Lanches.Where(p => p.Nome.ToLower().Contains(searchString.ToLower()));

                if (lanches.Any())
                {
                    categoriaAtual = "Lanches";
                }
                else
                {
                    categoriaAtual = "Nenhum lanche encontrado";
                }
            }

            return View("~/Views/Lanche/List.cshtml", new LancheListViewModel
            {
                Lanches = lanches,
                CategoriaAtual = categoriaAtual
            });



        }
    }
}
