namespace u2f
{
    internal class U2fRegisterResponseData
    {
        internal string Version { get; private set; }

        internal string AppId { get; private set; }
        internal string Challenge { get; private set; }

        internal U2fClientData ClientData { get; private set; }

        internal U2fRegistrationData RegistrationData { get; private set; }
    }
}
