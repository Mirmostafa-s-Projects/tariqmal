using System;
using System.Collections.Generic;
using System.Linq;
using Mohammad.Projects.TariqMal.Business.Entities;
using Mohammad.Projects.TariqMal.Business.Infrastructure;
using Mohammad.Projects.TariqMal.Business.Internals;
using Mohammad.Projects.TariqMal.Business.Model;
using Mohammad.Projects.TariqMal.Business.Model.HomePage;
using Mohammad.Projects.TariqMal.DataAccess.DataSources;
using Mohammad.Projects.TariqMal.DataAccess.Infrastructure;
using Mohammad.Projects.TariqMal.DataAccess.UI;

namespace Mohammad.Projects.TariqMal.Business.Site
{
    public class PagesEntity : AtmBusinessEntityBase<PagesEntity>
    {
        public (IEnumerable<MenuResultItem> Result, string Message) GetMainMenu(Language lang)
        {
            using (var dal = new MainMenuDalEntity())
            {
                var menus = dal.Select();

                var result = new List<MenuResultItem>();
                var all    = menus as HomePageMainMenu[] ?? menus.ToArray();
                result.AddRange(all.Where(m => m.ParentId == null).OrderBy(m => m.MenuOrder).Select(m => m.Convert(lang)));
                foreach (var root in result)
                foreach (var menu in all.Where(m => m.ParentId == root.Id).OrderBy(m => m.MenuOrder))
                    root.Children.Add(menu.Convert(lang));
                return (result, null);
            }
        }

        public (IEnumerable<Slider> Result, string Message) GetHomeSliderItems(Language arg) =>
            (new[]
            {
                new Slider
                {
                    Id      = Guid.Empty,
                    Title   = "لورم ایپسوم متن ساختگی",
                    Subject = "لورم ایپسوم متن ساختگی با تولید سادگی نامفهوم از صنعت چاپ و با استفاده از طراحان گرافیک است.",
                    Content =
                        "لورم ایپسوم متن ساختگی با تولید سادگی نامفهوم از صنعت چاپ و با استفاده از طراحان گرافیک است. چاپگرها و متون بلکه روزنامه و مجله در ستون و سطرآنچنان که لازم است و برای شرایط فعلی تکنولوژی مورد نیاز و کاربردهای متنوع با هدف بهبود ابزارهای کاربردی می باشد. کتابهای زیادی در شصت و سه درصد گذشته، حال و آینده شناخت فراوان جامعه و متخصصان را می طلبد تا با نرم افزارها شناخت بیشتری را برای طراحان رایانه ای علی الخصوص طراحان خلاقی و فرهنگ پیشرو در زبان فارسی ایجاد کرد. در این صورت می توان امید داشت که تمام و دشواری موجود در ارائه راهکارها و شرایط سخت تایپ به پایان رسد و زمان مورد نیاز شامل حروفچینی دستاوردهای اصلی و جوابگوی سوالات پیوسته اهل دنیای موجود طراحی اساسا مورد استفاده قرار گیرد.",
                    Image = Extensions.ToUrl("~/Images/Home_Slide_1.jpg")
                },
                new Slider
                {
                    Id      = Guid.Empty,
                    Title   = "لورم ایپسوم متن ساختگی",
                    Subject = "لورم ایپسوم متن ساختگی با تولید سادگی نامفهوم از صنعت چاپ و با استفاده از طراحان گرافیک است.",
                    Content =
                        "لورم ایپسوم متن ساختگی با تولید سادگی نامفهوم از صنعت چاپ و با استفاده از طراحان گرافیک است. چاپگرها و متون بلکه روزنامه و مجله در ستون و سطرآنچنان که لازم است و برای شرایط فعلی تکنولوژی مورد نیاز و کاربردهای متنوع با هدف بهبود ابزارهای کاربردی می باشد. کتابهای زیادی در شصت و سه درصد گذشته، حال و آینده شناخت فراوان جامعه و متخصصان را می طلبد تا با نرم افزارها شناخت بیشتری را برای طراحان رایانه ای علی الخصوص طراحان خلاقی و فرهنگ پیشرو در زبان فارسی ایجاد کرد. در این صورت می توان امید داشت که تمام و دشواری موجود در ارائه راهکارها و شرایط سخت تایپ به پایان رسد و زمان مورد نیاز شامل حروفچینی دستاوردهای اصلی و جوابگوی سوالات پیوسته اهل دنیای موجود طراحی اساسا مورد استفاده قرار گیرد.",
                    Image = Extensions.ToUrl("~/Images/Home_Slide_2.jpg")
                },
                new Slider
                {
                    Id      = Guid.Empty,
                    Title   = "لورم ایپسوم متن ساختگی",
                    Subject = "لورم ایپسوم متن ساختگی با تولید سادگی نامفهوم از صنعت چاپ و با استفاده از طراحان گرافیک است.",
                    Content =
                        "لورم ایپسوم متن ساختگی با تولید سادگی نامفهوم از صنعت چاپ و با استفاده از طراحان گرافیک است. چاپگرها و متون بلکه روزنامه و مجله در ستون و سطرآنچنان که لازم است و برای شرایط فعلی تکنولوژی مورد نیاز و کاربردهای متنوع با هدف بهبود ابزارهای کاربردی می باشد. کتابهای زیادی در شصت و سه درصد گذشته، حال و آینده شناخت فراوان جامعه و متخصصان را می طلبد تا با نرم افزارها شناخت بیشتری را برای طراحان رایانه ای علی الخصوص طراحان خلاقی و فرهنگ پیشرو در زبان فارسی ایجاد کرد. در این صورت می توان امید داشت که تمام و دشواری موجود در ارائه راهکارها و شرایط سخت تایپ به پایان رسد و زمان مورد نیاز شامل حروفچینی دستاوردهای اصلی و جوابگوی سوالات پیوسته اهل دنیای موجود طراحی اساسا مورد استفاده قرار گیرد.",
                    Image = Extensions.ToUrl("~/Images/Home_Slide_3.jpg")
                },
                new Slider
                {
                    Id      = Guid.Empty,
                    Title   = "لورم ایپسوم متن ساختگی",
                    Subject = "لورم ایپسوم متن ساختگی با تولید سادگی نامفهوم از صنعت چاپ و با استفاده از طراحان گرافیک است.",
                    Content =
                        "لورم ایپسوم متن ساختگی با تولید سادگی نامفهوم از صنعت چاپ و با استفاده از طراحان گرافیک است. چاپگرها و متون بلکه روزنامه و مجله در ستون و سطرآنچنان که لازم است و برای شرایط فعلی تکنولوژی مورد نیاز و کاربردهای متنوع با هدف بهبود ابزارهای کاربردی می باشد. کتابهای زیادی در شصت و سه درصد گذشته، حال و آینده شناخت فراوان جامعه و متخصصان را می طلبد تا با نرم افزارها شناخت بیشتری را برای طراحان رایانه ای علی الخصوص طراحان خلاقی و فرهنگ پیشرو در زبان فارسی ایجاد کرد. در این صورت می توان امید داشت که تمام و دشواری موجود در ارائه راهکارها و شرایط سخت تایپ به پایان رسد و زمان مورد نیاز شامل حروفچینی دستاوردهای اصلی و جوابگوی سوالات پیوسته اهل دنیای موجود طراحی اساسا مورد استفاده قرار گیرد.",
                    Image = Extensions.ToUrl("~/Images/Home_Slide_4.jpg")
                }
            }, null);

        public (IEnumerable<ServiceResultItem> Result, string Message) GetHomeServices(Language lang)
        {
            using (var dal = new ServiceDalEntity())
            {
                return (dal.Select().Select(service => service.Convert(lang)).ToList(), null);
            }
        }

        public (NewsResultSet Result, string Message) GetHomeNews(Language lang) => NewsEntity.Instance.Get(lang, new Pagination(0, 4));

        public (ContactUsResultItem Result, string Message) GetContactUs(Language lang) =>
            (new ContactUsResultItem
            {
                Data = new List<(string Key, string Value)>
                {
                    (TranslationEntity.Translate("Mobile Phone1", Language.En, lang), "+989362809027"),
                    (TranslationEntity.Translate("Mobile Phone2", Language.En, lang), "+989352538233"),
                    (TranslationEntity.Translate("Fixed Phone1",  Language.En, lang), "+982133944944-5"),
                    (
                        TranslationEntity.Translate("Address", Language.En, lang),
                        TranslationEntity.Translate("ایران، تهران، شهر ری، دولت آباد، بلوار قدس، اول خیابان یاس", Language.En,
                                                    lang)
                    )
                }
            }, null);
    }
}