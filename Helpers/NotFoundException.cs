﻿namespace CourseSystem.Helpers;

public class NotFoundException : Exception
{
   public NotFoundException(string message) : base(message){}
}