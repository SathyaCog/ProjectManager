﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManager.InterfaceLayer
{
    public interface ITaskBL
    {
        void AddTask(CommonEntities.Tasks task);
        void AddParentTask(CommonEntities.ParentTasks pTask);
        Collection<CommonEntities.ParentTasks> GetParentTasks();
    }
}
