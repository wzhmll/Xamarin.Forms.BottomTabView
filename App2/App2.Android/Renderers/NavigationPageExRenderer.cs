﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;


using Xamarin.Forms;
using Xamarin.Forms.Platform.Android.AppCompat;

using App2.Controls.WithRenderer;
using App2.Droid.Renderers;
using Xamarin.Forms.Platform.Android;
using System.ComponentModel;
using System.Reflection;

[assembly: ExportRenderer(typeof(NavigationPageEx), typeof(NavigationPageExRenderer))]
namespace App2.Droid.Renderers
{
    public class NavigationPageExRenderer : NavigationPageRenderer
    {
        Android.Support.V7.Widget.Toolbar FatherToolbar;
        TextView initTextView;

        protected override void OnElementChanged(ElementChangedEventArgs<NavigationPage> e)
        {
            base.OnElementChanged(e);
            FatherToolbar = (Android.Support.V7.Widget.Toolbar)typeof(NavigationPageRenderer)
                    .GetField("_toolbar", BindingFlags.NonPublic | BindingFlags.Instance)
                    .GetValue(this);
            InitToolBarMiddleText();
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);
            var navi = (NavigationPageEx)Element;
            if(e.PropertyName == NavigationPageEx.MiddleTitleColorProperty.PropertyName)             
                UpdateToolBarMiddleText(navi.MiddleTitleColor);
            if (e.PropertyName == NavigationPageEx.MiddleTitleTextProperty.PropertyName)
                UpdateToolBarMiddleText(navi.MiddleTitleText);
        }

        void InitToolBarMiddleText()
        {
            var toolbar = FatherToolbar;
            if (toolbar != null)
            {
                string toolbarTitle = "TiTle";
                var layoutPara = new Android.Support.V7.Widget.Toolbar.LayoutParams(
                    LayoutParams.WrapContent, LayoutParams.WrapContent);
                layoutPara.Gravity = 1;
                initTextView = new TextView(Forms.Context)
                {
                    LayoutParameters = layoutPara,
                    Gravity = GravityFlags.Center,
                    Text = toolbarTitle ?? "",
                };
                initTextView.SetTextColor(Android.Graphics.Color.White);
                initTextView.TextSize = 20;               
                toolbar.AddView(initTextView);
                toolbar.Title = "";

            }
        }

        void UpdateToolBarMiddleText(Color color)
        {
            var toolbar = FatherToolbar;
            if (toolbar != null && initTextView != null)
            {
                initTextView.SetTextColor(color.ToAndroid());
                toolbar.Title = "";
            }
        }

        void UpdateToolBarMiddleText(string text)
        {
            var toolbar = FatherToolbar;
            if (toolbar != null && initTextView != null)
            {
                initTextView.Text = text;
                toolbar.Title = "";
            }
        }

    }

}
