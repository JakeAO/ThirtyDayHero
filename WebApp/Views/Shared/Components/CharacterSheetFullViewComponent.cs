using Microsoft.AspNetCore.Mvc;
using ThirtyDayHero;

namespace WebApp.Views.Shared.Components
{
    [ViewComponent(Name = "CharacterSheetFull")]
    public class CharacterSheetFullViewComponent : ViewComponent
    {
        public ICharacterActor Actor { get; private set; }

        public IViewComponentResult Invoke(ICharacterActor actor)
        {
            Actor = actor;
            return View(this);
        }
    }
}