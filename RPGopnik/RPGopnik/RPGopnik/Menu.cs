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

        public virtual void Update()
        {
            foreach (Content ct in content_list)
                ct.Update(Mouse.GetState());
        }

        public virtual void Draw(SpriteBatch spritebatch)
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

    class EndMenu : Menu
    {
        public EndMenu() : base(new List<Content> {  ContentLoader.end_menu.background,
                                                     ContentLoader.end_menu.aue_button})
        {

        }
    }

    class Story1 : Menu
    {
        public Story1() : base(new List<Content> { ContentLoader.story_content.story1,
                                                        ContentLoader.story_content.continue_button })
        { }
    }

    class Story2 : Menu
    {
        public Story2() : base(new List<Content> { ContentLoader.story_content.story2,
                                                        ContentLoader.story_content.game_button })
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

    class SLScreen : Menu
    {
        public static List<Content> unlockedAt100xp_content;
        public static List<Content> unlockedAt200xp_content;
        public static List<Content> unlockedIcons_content;
        private Character character;
        public SLScreen(Game game) : base(new List<Content> { ContentLoader.pause_content.background,
                                                              ContentLoader.sl_menu_content.spells_window,
                                                              ContentLoader.sl_menu_content.resume_button })
        {
            character = game.character;
            unlockedAt100xp_content = new List<Content> { ContentLoader.sl_menu_content.add_hp,
                                                              ContentLoader.sl_menu_content.antidote,
                                                              ContentLoader.sl_menu_content.heal,
                                                              ContentLoader.sl_menu_content.move };

            unlockedAt200xp_content = new List<Content> { ContentLoader.sl_menu_content.revive,
                                                              ContentLoader.sl_menu_content.shield };
            unlockedIcons_content = new List<Content> { };
        }

        public override void Update()
        {
            base.Update();
            if (character.XP >= 100)
            {
                for(int i = 0; i < unlockedAt100xp_content.Count; i++)
                    unlockedAt100xp_content[i].Update(Mouse.GetState());
            }
            if (character.XP >= 200)
            {
                for (int i = 0; i < unlockedAt200xp_content.Count; i++)
                    unlockedAt200xp_content[i].Update(Mouse.GetState());
            }
        }

        public override void Draw(SpriteBatch spritebatch)
        {
            base.Draw(spritebatch);
            spritebatch.Begin();
            if (character.XP >= 100)
            {
                foreach (Content ct in unlockedAt100xp_content)
                {
                    ct.Draw(spritebatch);
                }
            }
            if (character.XP >= 200)
            {
                foreach (Content ct in unlockedAt200xp_content)
                {
                    ct.Draw(spritebatch);
                }
            }
            foreach(Content ct in unlockedIcons_content)
            {
                ct.Draw(spritebatch);
            }
            spritebatch.End();
        }
    }
}
