namespace u2f
{
    internal class U2fRegisterResponse
    {
        internal string Type { get; private set; }

        internal U2fRegisterResponseData ResponseData { get; private set; }
    }
}
