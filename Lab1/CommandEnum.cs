﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab1
{
    public enum CommandEnum
    {
        AddTransaction = 1,
        AddBlock,
        AddNode,
        ChangeCurrentNode,
        ShowBalances,
        ShowBlockChain,
        ShowLastBLock,
        SyncBlockchain
    }
}
