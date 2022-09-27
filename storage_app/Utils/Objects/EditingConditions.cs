namespace storage_app.Utils.Objects
{
    public class EditingConditions
    {
        public bool IsEditing { get; set; } = false;
        public bool IsAdding { get; set; } = false;
        public bool IsSavable
        {
            get { return IsEditing && !IsAdding; }
        }
        public bool IsAddable
        {
            get { return IsEditing && IsAdding; }
        }
        public bool IsCancelable
        {
            get { return IsAddable || IsSavable; }
        }

        public bool IsNotEditing
        {
            get { return !IsEditing; }
        }
    }
}
