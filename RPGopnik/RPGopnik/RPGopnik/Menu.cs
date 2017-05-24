using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace RPGopnik
{
    class Menu
    {
        List<Content> content_list;

        public Menu(List<Content> content_list)
        {
            this.content_list = content_list;
        }

        public void Update()
        {
            foreach (Content ct in content_list)
                ct.Update(Mouse.GetState());
        }

        public void Draw(SpriteBatch spritebatch)
        {
            spritebatch.Begin();
            foreach (Content ct in content_list)
                ct.Draw(spritebatch);
            spritebatch.End();
        }
    }

    class HelpMenu : Menu
    {
        public HelpMenu() : base(new List<Content> { ContentLoader.menu_content.background,
                                                     ContentLoader.menu_content.logo,
                                                     ContentLoader.help_menu_content.info,
                                                     ContentLoader.help_menu_content.main_menu })
        { }
    }

    class MainMenu : Menu
    {
        public MainMenu() : base(new List<Content> { ContentLoader.menu_content.background,
                                                     ContentLoader.menu_content.logo,
                                                     ContentLoader.main_menu_content.game_button,
                                                     ContentLoader.main_menu_content.help_button,
                                                     ContentLoader.main_menu_content.exit_button})
        { }
    }

    class Pause : Menu
    {
        public Pause() : base(new List<Content> { ContentLoader.pause_content.background,
                                                  ContentLoader.pause_content.pause_header,
                                                  ContentLoader.pause_menu_content.resume_button,
                                                  ContentLoader.pause_menu_content.exit_button })
        { }
    }
}
