namespace u2f
{
    internal class U2fResponse<TResponseData>
    {
        internal string Type { get; private set; }

        internal TResponseData ResponseData { get; private set; }
    }
}
