using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace OathKeeper
{
    /// <summary>
    /// このカスタム コントロールを XAML ファイルで使用するには、手順 1a または 1b の後、手順 2 に従います。
    ///
    /// 手順 1a) 現在のプロジェクトに存在する XAML ファイルでこのカスタム コントロールを使用する場合
    /// この XmlNamespace 属性を使用場所であるマークアップ ファイルのルート要素に
    /// 追加します:
    ///
    ///     xmlns:MyNamespace="clr-namespace:OathKeeper"
    ///
    ///
    /// 手順 1b) 異なるプロジェクトに存在する XAML ファイルでこのカスタム コントロールを使用する場合
    /// この XmlNamespace 属性を使用場所であるマークアップ ファイルのルート要素に
    /// 追加します:
    ///
    ///     xmlns:MyNamespace="clr-namespace:OathKeeper;assembly=OathKeeper"
    ///
    /// また、XAML ファイルのあるプロジェクトからこのプロジェクトへのプロジェクト参照を追加し、
    /// リビルドして、コンパイル エラーを防ぐ必要があります:
    ///
    ///     ソリューション エクスプローラーで対象のプロジェクトを右クリックし、
    ///     [参照の追加] の [プロジェクト] を選択してから、このプロジェクトを参照し、選択します。
    ///
    ///
    /// 手順 2)
    /// コントロールを XAML ファイルで使用します。
    ///
    ///     <MyNamespace:RulerControl/>
    ///
    /// </summary>
    /// 

    public enum ERulerPriority
    {
        VERTICAL,
        HORIZONTAL
    }

    public enum ERulerDirectionX
    {
        LEFT_TO_RIGHT,
        RIGHT_TO_LEFT
    }
    public enum ERulerDirectionY
    {
        TOP_TO_BOTTOM,
        BOTTOM_TO_TOP
    }


    public class RulerControl : Control
    {
        static RulerControl()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(RulerControl), new FrameworkPropertyMetadata(typeof(RulerControl)));

            short_line = new RulerLine(390f, Brushes.Gray, 0.5f);
            middle_line = new RulerLine(0.5f, Brushes.Black, 0.1f);
            long_line = new RulerLine(390f, Brushes.Black, 1.4f);
        }

        #region Ruler Line
        public class RulerLine
        {
            #region RulerLine constructors
            public RulerLine()
            {
                length = 0;
                color = Brushes.Black;
                thickness = 0;
            }
            public RulerLine(float in_length)
            {
                length = in_length;
                color = Brushes.Black;
            }
            public RulerLine(float in_length, SolidColorBrush in_color)
            {
                length = 0;
                color = in_color;

            }
            public RulerLine(float in_length, SolidColorBrush in_color, float in_thickness)
            {
                length = in_length;
                color = in_color;
                thickness = in_thickness;
            }
            #endregion

            //目盛りの長さ
            public float length { get; set; }
            //目盛りの太さ
            public float thickness { get; set; }
            //目盛りの色
            public SolidColorBrush color { get; set; }
        }
        #endregion

        static RulerLine short_line { get; set; }
        static RulerLine middle_line { get; set; }
        static RulerLine long_line { get; set; }

        float short_line_interval_length { get; set; } = 20.2f;
        //public int long_line_interval_count { get; set; } =4;

        //bool is_middleline_drawable = false;
        protected override void OnRender(DrawingContext drawingContext)
        {
            //if()
            Vector offset = new Vector(0, 0);
            Vector direction = new Vector(0, 0);
            Vector line_extend_direction = new Vector(0,0);
            float actul_length = 0;

            if(ruler_priority==ERulerPriority.HORIZONTAL)
            {
                offset.X = (ruler_direction_x == ERulerDirectionX.RIGHT_TO_LEFT ? ActualWidth : 0);
                offset.Y = (ruler_direction_y == ERulerDirectionY.BOTTOM_TO_TOP ? ActualHeight : 0);
                direction.X = (ruler_direction_x == ERulerDirectionX.RIGHT_TO_LEFT ? -1 : 1);
                line_extend_direction.Y = ruler_direction_y == ERulerDirectionY.BOTTOM_TO_TOP ? 1.0f : -1.0f;
                actul_length = (float)ActualWidth;
            }
            else
            {
                offset.X = (ruler_direction_x == ERulerDirectionX.RIGHT_TO_LEFT ? ActualWidth : 0);
                offset.Y = (ruler_direction_y == ERulerDirectionY.BOTTOM_TO_TOP ? ActualHeight : 0);
                direction.Y = (ruler_direction_y == ERulerDirectionY.BOTTOM_TO_TOP ? -1 : 1);
                actul_length = (float)ActualHeight;
                line_extend_direction.X = (ruler_direction_x == ERulerDirectionX.LEFT_TO_RIGHT ? 1.0f : -1.0f);
            }

            for(int i = 0; i < actul_length / long_line_interval_length; i++)
            {

                Vector line_root = i * direction* long_line_interval_length + offset;
                Vector line_top = line_root + line_extend_direction * long_line.length;
                drawingContext.DrawLine(new Pen(long_line.color, long_line.thickness), new Point(line_root.X, line_root.Y), new Point(line_top.X, line_top.Y));


                for(int n=1;n<long_line_division_count;n++)
                {
                    Vector short_line_root = direction * long_line_interval_length*((float)n/ (float)long_line_division_count) + line_root;
                    Vector short_line_top = short_line_root + line_extend_direction * short_line.length;
                    drawingContext.DrawLine(new Pen(short_line.color, short_line.thickness), new Point(short_line_root.X, short_line_root.Y), new Point(short_line_top.X, short_line_top.Y));

                }
            }


        }


        public static readonly DependencyProperty IsMiddleLineDrawable =
           DependencyProperty.Register("is_middleline_drawable", typeof(bool), typeof(RulerControl),
           new UIPropertyMetadata(false));

        public bool is_middleline_drawable
        {
            get { return (bool)base.GetValue(IsMiddleLineDrawable); }
            set { this.SetValue(IsMiddleLineDrawable, value); }
        }

        public static readonly DependencyProperty LongLineDivisionCount =
           DependencyProperty.Register("long_line_division_count", typeof(int), typeof(RulerControl),
           new UIPropertyMetadata(4));

        public int long_line_division_count
        {
            get { return (int)base.GetValue(LongLineDivisionCount); }
            set { this.SetValue(LongLineDivisionCount, value); }
        }

        public static readonly DependencyProperty LongLineIntervalLength =
         DependencyProperty.Register("long_line_interval_length", typeof(float), typeof(RulerControl),
         new UIPropertyMetadata(100.0f));

        public float long_line_interval_length
        {
            get { return (float)base.GetValue(LongLineIntervalLength); }
            set { this.SetValue(LongLineIntervalLength, value); }
        }

        #region parameter::ruler_priority
        public static readonly DependencyProperty RulerDirectionPriority =
         DependencyProperty.Register("ruler_priority", typeof(ERulerPriority), typeof(RulerControl),
         new UIPropertyMetadata(ERulerPriority.HORIZONTAL));

        public ERulerPriority ruler_priority
        {
            get { return (ERulerPriority)base.GetValue(RulerDirectionPriority); }
            set { this.SetValue(RulerDirectionPriority, value); }
        }
        #endregion


        #region parameter::ruler_direction x
        public static readonly DependencyProperty RulerDirectionX =
         DependencyProperty.Register("ruler_direction_x", typeof(ERulerDirectionX), typeof(RulerControl),
         new UIPropertyMetadata(ERulerDirectionX.LEFT_TO_RIGHT));

        public ERulerDirectionX ruler_direction_x
        {
            get { return (ERulerDirectionX)base.GetValue(RulerDirectionX); }
            set { this.SetValue(RulerDirectionX, value); }
        }
        #endregion
        #region parameter::ruler_direction y
        public static readonly DependencyProperty RulerDirectionY =
         DependencyProperty.Register("ruler_direction_y", typeof(ERulerDirectionY), typeof(RulerControl),
         new UIPropertyMetadata(ERulerDirectionY.TOP_TO_BOTTOM));

        public ERulerDirectionY ruler_direction_y
        {
            get { return (ERulerDirectionY)base.GetValue(RulerDirectionY); }
            set { this.SetValue(RulerDirectionY, value); }
        }
        #endregion

    }
}

