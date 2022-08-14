﻿namespace Notes.Tests.Common
{
    public class TestCommandBase : IDisposable
    {
        protected readonly NotesDbContext Context;

        public TestCommandBase()
        {
            Context = NotesContextFactory.Create();
        }

        public void Dispose()
        {
            NotesContextFactory.Delete(Context);
        }
    }
}
