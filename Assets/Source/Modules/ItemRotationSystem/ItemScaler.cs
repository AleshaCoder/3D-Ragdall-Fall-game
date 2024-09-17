namespace RotationSystem
{
    public class ItemScaler
    {
        private IScalable _scalable;

        public void SetScalable(IScalable scalable)
        {
            _scalable = scalable;
        }

        public void FreeScalable()
        {
            _scalable = null;
        }

        public void Scale(float scale)
        {
            if (_scalable == null)
                return;

            if (_scalable.CanScale)
                _scalable.Scale(scale);
        }
    }
}
