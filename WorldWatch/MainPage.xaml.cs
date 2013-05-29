using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// 空白ページのアイテム テンプレートについては、http://go.microsoft.com/fwlink/?LinkId=234238 を参照してください

namespace WorldWatch
{
    /// <summary>
    /// それ自体で使用できる空白ページまたはフレーム内に移動できる空白ページ。
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();

            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(0.5);
            timer.Tick += timer_Tick;
            timer.Start();

            SizeChanged += MainPage_SizeChanged;
        }

        void timer_Tick(object sender, object e)
        {
            DateTimeOffset time = DateTimeOffset.UtcNow;
            Tokyo.DataContext = TimeContext(time, "東京（日本）", 9);
            Beijing.DataContext = TimeContext(time, "北京(中国)", 8);
            Dubai.DataContext = TimeContext(time, "ドバイ（U.A.E.）", 4);
            London.DataContext = TimeContext(time, "ロンドン（イギリス）", 0);
            NewYork.DataContext = TimeContext(time, "ニューヨーク（米国）", -4);
            SanFrancisco.DataContext = TimeContext(time, "サンフランシスコ（米国）", -7);
        }

        private object TimeContext(DateTimeOffset time, string place,  double offset)
        {
            DateTimeOffset localtime = time.ToOffset(TimeSpan.FromHours(offset));
            return new { 
                Place = place,
                Time = localtime.ToString("HH:mm:ss"),
                Date = localtime.ToString("yyyy-MM-dd"),
            }; 
        }

        /// <summary>
        /// このページがフレームに表示されるときに呼び出されます。
        /// </summary>
        /// <param name="e">このページにどのように到達したかを説明するイベント データ。Parameter 
        /// プロパティは、通常、ページを構成するために使用します。</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
        }

        void MainPage_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            string viewstate = ApplicationView.Value.ToString();
            VisualStateManager.GoToState(this, viewstate, false);
        }
    }
}
