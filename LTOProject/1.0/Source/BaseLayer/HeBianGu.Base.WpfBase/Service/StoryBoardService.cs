#region <版 本 注 释>
/*
 * ========================================================================
 * Copyright(c) 长虹智慧健康有限公司, All Rights Reserved.
 * ========================================================================
 *    
 * 作者：[李海军]   时间：2017/12/26 18:37:37 
 * 文件名：StoryBoardService 
 * 说明：
 * 
 * 
 * 修改者：           时间：               
 * 修改说明：
 * ========================================================================
*/
#endregion
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Animation;

namespace HeBianGu.Base.WpfBase
{
    public class StoryBoardService
    {

        /// <summary>
        /// 喷泉效果
        /// </summary>
        /// <param name="cav">画布</param>
        /// <param name="uclist">展示集合</param>
        /// <param name="pL">喷出点左</param>
        /// <param name="pT">喷出点上</param>
        /// <param name="Mul">放大倍数</param>
        /// <param name="middle_value">放大时间点</param>
        /// <param name="end_value">还原时间点</param>
        public static void FountainAnimation(List<DependencyObject> uclist, double pL = 0, double pT = 0, double Mul = 10, double middle_value = 0.5, double end_value = 1)
        {
            if (uclist.Count <= 0)
            {
                return;
            }
            Storyboard storyboard = new Storyboard();

            double Init = 0;
            double Org = 1;
            double first_value = 0;
            Random r2 = new Random();

            for (int i = 0; i < uclist.Count; i++)
            {
                double first = i * 0.05 + first_value;
                double middle = i * 0.05 + middle_value;
                double end = i * 0.05 + end_value;

                var c = uclist[i];

                EasingDoubleKeyFrame edf0 = new EasingDoubleKeyFrame(0, KeyTime.FromTimeSpan(TimeSpan.FromSeconds(0)));//所有元素起点都是0
                EasingDoubleKeyFrame edf1 = new EasingDoubleKeyFrame(Init, KeyTime.FromTimeSpan(TimeSpan.FromSeconds(first)));
                EasingDoubleKeyFrame edf2 = new EasingDoubleKeyFrame(Mul, KeyTime.FromTimeSpan(TimeSpan.FromSeconds(middle)));
                EasingDoubleKeyFrame edf3 = new EasingDoubleKeyFrame(Org, KeyTime.FromTimeSpan(TimeSpan.FromSeconds(end)));

                DoubleAnimationUsingKeyFrames daukf1 = new DoubleAnimationUsingKeyFrames();
                daukf1.KeyFrames.Add(edf0);
                daukf1.KeyFrames.Add(edf1);
                daukf1.KeyFrames.Add(edf2);
                daukf1.KeyFrames.Add(edf3);
                storyboard.Children.Add(daukf1);
                Storyboard.SetTarget(daukf1, c);
                Storyboard.SetTargetProperty(daukf1, new PropertyPath("(UIElement.RenderTransform).(TransformGroup.Children)[0].(ScaleTransform.ScaleX)"));

                DoubleAnimationUsingKeyFrames daukf2 = new DoubleAnimationUsingKeyFrames();
                daukf2.KeyFrames.Add(edf0);
                daukf2.KeyFrames.Add(edf1);
                daukf2.KeyFrames.Add(edf2);
                daukf2.KeyFrames.Add(edf3);
                storyboard.Children.Add(daukf2);
                Storyboard.SetTarget(daukf2, c);
                Storyboard.SetTargetProperty(daukf2, new PropertyPath("(UIElement.RenderTransform).(TransformGroup.Children)[0].(ScaleTransform.ScaleY)"));

                DoubleAnimationUsingKeyFrames daukf3 = new DoubleAnimationUsingKeyFrames();
                EasingDoubleKeyFrame edf31 = new EasingDoubleKeyFrame(r2.Next(-1000, 1000), KeyTime.FromTimeSpan(TimeSpan.FromSeconds(0)));
                //EasingDoubleKeyFrame edf31 = new EasingDoubleKeyFrame(r.Next(200, 1000), KeyTime.FromTimeSpan(TimeSpan.FromSeconds(middle)));
                EasingDoubleKeyFrame edf32 = new EasingDoubleKeyFrame(0, KeyTime.FromTimeSpan(TimeSpan.FromSeconds(end)));
                daukf3.KeyFrames.Add(edf31);
                daukf3.KeyFrames.Add(edf32);
                storyboard.Children.Add(daukf3);
                Storyboard.SetTarget(daukf3, c);
                Storyboard.SetTargetProperty(daukf3, new PropertyPath("(UIElement.RenderTransform).(TransformGroup.Children)[3].(TranslateTransform.X)"));

                DoubleAnimationUsingKeyFrames daukf4 = new DoubleAnimationUsingKeyFrames();
                EasingDoubleKeyFrame edf41 = new EasingDoubleKeyFrame(r2.Next(-1000, 1000), KeyTime.FromTimeSpan(TimeSpan.FromSeconds(0)));
                //EasingDoubleKeyFrame edf41 = new EasingDoubleKeyFrame(r2.Next(200, 1000), KeyTime.FromTimeSpan(TimeSpan.FromSeconds(middle)));
                EasingDoubleKeyFrame edf42 = new EasingDoubleKeyFrame(0, KeyTime.FromTimeSpan(TimeSpan.FromSeconds(end)));
                daukf4.KeyFrames.Add(edf41);
                daukf4.KeyFrames.Add(edf42);
                storyboard.Children.Add(daukf4);
                Storyboard.SetTarget(daukf4, c);
                Storyboard.SetTargetProperty(daukf4, new PropertyPath("(UIElement.RenderTransform).(TransformGroup.Children)[3].(TranslateTransform.Y)"));
            }
            storyboard.FillBehavior = FillBehavior.HoldEnd;
            storyboard.Begin();
        }
    }
}
