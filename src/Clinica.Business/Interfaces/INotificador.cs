﻿using Clinica.Business.Notificacoes;
using System;
using System.Collections.Generic;
using System.Text;

namespace Clinica.Business.Interfaces
{
    public interface INotificador
    {
        bool TemNotificacao();
        List<Notificacao> ObterNotificacoes();
        void Handle(Notificacao notificacao);
    }
}
