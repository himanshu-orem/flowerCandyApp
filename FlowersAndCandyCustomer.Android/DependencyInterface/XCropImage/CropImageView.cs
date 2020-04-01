using System;
using System.Collections.Generic;
using Android.Content;
using Android.Graphics;
using Android.Util;
using Android.Views;

namespace FlowersAndCandyCustomer.Droid.SourceCode.XCropImage
{
    public sealed class CropImageView : ImageViewTouchBase
    {
        #region Private members

        private readonly List<HighlightView> _hightlightViews = new List<HighlightView>();
        private HighlightView _mMotionHighlightView;
        private float _mLastX;
        private float _mLastY;
        private HighlightView.HitPosition _motionEdge;
        private readonly Context _context;

        #endregion

        #region Constructor

        public CropImageView(Context context, IAttributeSet attrs) : base(context, attrs)
        {
            try
            {

           
            SetLayerType(LayerType.Software, null);
            _context = context;
            }
            catch (Exception)
            {
            }
        }

        #endregion

        #region Public methods

        public void ClearHighlightViews()
        {
            try
            {

           
            _hightlightViews.Clear();
            }
            catch (Exception)
            {
            }
        }

        public void AddHighlightView(HighlightView hv)
        {
            try
            {

         
            _hightlightViews.Add(hv);
            Invalidate();
            }
            catch (Exception)
            {
            }
        }

        #endregion

        #region Overrides

        protected override void OnDraw(Canvas canvas)
        {
            try
            {

           
            base.OnDraw(canvas);

            foreach (var t in _hightlightViews)
            {
                t.Draw(canvas);
            }
            }
            catch (Exception)
            {
            }
        }

        protected override void OnLayout(bool changed, int left, int top, int right, int bottom)
        {
            try
            {

           
            base.OnLayout(changed, left, top, right, bottom);

            if (bitmapDisplayed.Bitmap != null)
            {
                foreach (var hv in _hightlightViews)
                {
                    hv.MatrixImage.Set(ImageMatrix);
                    hv.Invalidate();

                    if (hv.Focused)
                    {
                        CenterBasedOnHighlightView(hv);
                    }
                }
            }
            }
            catch (Exception)
            {
            }
        }

        protected override void ZoomTo(float scale, float centerX, float centerY)
        {
            try
            {

           
            base.ZoomTo(scale, centerX, centerY);
            foreach (var hv in _hightlightViews)
            {
                hv.MatrixImage.Set(ImageMatrix);
                hv.Invalidate();
            }
            }
            catch (Exception)
            {
            }
        }

        protected override void ZoomIn()
        {
            try
            {

           
            base.ZoomIn();
            foreach (var hv in _hightlightViews)
            {
                hv.MatrixImage.Set(ImageMatrix);
                hv.Invalidate();
            }
            }
            catch (Exception)
            {
            }
        }

        protected override void ZoomOut()
        {
            try
            {

           
            base.ZoomOut();
            foreach (var hv in _hightlightViews)
            {
                hv.MatrixImage.Set(ImageMatrix);
                hv.Invalidate();
            }
            }
            catch (Exception)
            {
            }
        }

        protected override void PostTranslate(float deltaX, float deltaY)
        {
            try
            {

           
            base.PostTranslate(deltaX, deltaY);
            foreach (var hv in _hightlightViews)
            {
                hv.MatrixImage.PostTranslate(deltaX, deltaY);
                hv.Invalidate();
            }
            }
            catch (Exception)
            {
            }
        }

        public override bool OnTouchEvent(MotionEvent ev)
        {
            try
            {

            var cropImage = (CropImage)_context;
            if (cropImage.Saving)
            {
                return false;
            }

            switch (ev.Action)
            {
                case MotionEventActions.Down:
                  
                    foreach (var hv in _hightlightViews)
                    {
                        var edge = hv.GetHit(ev.GetX(), ev.GetY());
                        if (edge != HighlightView.HitPosition.None)
                        {
                            _motionEdge = edge;
                            _mMotionHighlightView = hv;
                            _mLastX = ev.GetX();
                            _mLastY = ev.GetY();
                            _mMotionHighlightView.Mode = 
                                (edge == HighlightView.HitPosition.Move)
                                    ? HighlightView.ModifyMode.Move
                                    : HighlightView.ModifyMode.Grow;
                            break;
                        }
                    }
                    break;

                case MotionEventActions.Up:
                    if (_mMotionHighlightView != null)
                    {
                        CenterBasedOnHighlightView(_mMotionHighlightView);
                        _mMotionHighlightView.Mode = HighlightView.ModifyMode.None;
                    }

                    _mMotionHighlightView = null;
                    break;

                case MotionEventActions.Move:
                  if (_mMotionHighlightView != null)
                    {
                        _mMotionHighlightView.HandleMotion(_motionEdge,
                                                          ev.GetX() - _mLastX,
                                                          ev.GetY() - _mLastY);
                        _mLastX = ev.GetX();
                        _mLastY = ev.GetY();

                        if (true)
                        {
                            // This section of code is optional. It has some user
                            // benefit in that moving the crop rectangle against
                            // the edge of the screen causes scrolling but it means
                            // that the crop rectangle is no longer fixed under
                            // the user's finger.
                            EnsureVisible(_mMotionHighlightView);
                        }
                    }
                    break;
            }

            switch (ev.Action)
            {
                case MotionEventActions.Up:
                    Center(true, true);
                    break;
                case MotionEventActions.Move:
                    // if we're not zoomed then there's no point in even allowing
                    // the user to move the image around.  This call to center puts
                    // it back to the normalized location (with false meaning don't
                    // animate).
                    if (Math.Abs(GetScale() - 1F) < double.Epsilon)
                    {
                        Center(true, true);
                    }
                    break;
            }

            }
            catch (Exception)
            {
            }

            return true;
        }

        #endregion

        #region Private helpers

        // Pan the displayed image to make sure the cropping rectangle is visible.
        private void EnsureVisible(HighlightView hv)
        {
            try
            {

          
            var r = hv.DrawRect;

            var panDeltaX1 = Math.Max(0, IvLeft - r.Left);
            var panDeltaX2 = Math.Min(0, IvRight - r.Right);

            var panDeltaY1 = Math.Max(0, IvTop - r.Top);
            var panDeltaY2 = Math.Min(0, IvBottom - r.Bottom);

            var panDeltaX = panDeltaX1 != 0 ? panDeltaX1 : panDeltaX2;
            var panDeltaY = panDeltaY1 != 0 ? panDeltaY1 : panDeltaY2;

            if (panDeltaX != 0 || panDeltaY != 0)
            {
                PanBy(panDeltaX, panDeltaY);
            }
            }
            catch (Exception)
            {
            }
        }

        // If the cropping rectangle's size changed significantly, change the
        // view's center and scale according to the cropping rectangle.
        private void CenterBasedOnHighlightView(HighlightView hv)
        {
            try
            {

           
            var drawRect = hv.DrawRect;

            float width = drawRect.Width();
            float height = drawRect.Height();

            float thisWidth = Width;
            float thisHeight = Height;

            var z1 = thisWidth / width * .6F;
            var z2 = thisHeight / height * .6F;

            var zoom = Math.Min(z1, z2);
            zoom = zoom * GetScale();
            zoom = Math.Max(1F, zoom);
            if ((Math.Abs(zoom - GetScale()) / zoom) > .1)
            {
                float[] coordinates = {
                    hv.CropRect.CenterX(),
					hv.CropRect.CenterY()
				};

                ImageMatrix.MapPoints(coordinates);
                ZoomTo(zoom, coordinates[0], coordinates[1], 300F);
            }

            EnsureVisible(hv);
            }
            catch (Exception)
            {
            }
        }

        #endregion
    }
}
