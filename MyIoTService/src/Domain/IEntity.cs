﻿using System;
using System.Collections.Generic;
using System.Text;

namespace MyIoTService.Domain
{
    public interface IEntity<T>
    {
        public T Id { get; set; }
    }
}
