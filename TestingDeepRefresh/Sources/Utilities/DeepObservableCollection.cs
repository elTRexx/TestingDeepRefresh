﻿using System.Collections;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;

namespace TestingDeepRefresh.Sources.Utilities
{
  /// <summary>
  /// see: https://stackoverflow.com/questions/51591536/raising-event-when-property-in-observablecollection-changes
  /// </summary>
  /// <typeparam name="T"></typeparam>
  public class DeepObservableCollection<T> : ObservableCollection<T>
    where T : INotifyPropertyChanged
  {
    protected override void OnCollectionChanged(NotifyCollectionChangedEventArgs e)
    {
      if (e.Action == NotifyCollectionChangedAction.Add)
      {
        RegisterPropertyChanged(e.NewItems);
      }
      else if (e.Action == NotifyCollectionChangedAction.Remove)
      {
        UnRegisterPropertyChanged(e.OldItems);
      }
      else if (e.Action == NotifyCollectionChangedAction.Replace)
      {
        UnRegisterPropertyChanged(e.OldItems);
        RegisterPropertyChanged(e.NewItems);
      }

      base.OnCollectionChanged(e);
    }

    protected override void ClearItems()
    {
      UnRegisterPropertyChanged(this);
      base.ClearItems();
    }

    private void RegisterPropertyChanged(IList items)
    {
      foreach (INotifyPropertyChanged item in items)
      {
        if (item != null)
        {
          item.PropertyChanged += new PropertyChangedEventHandler(item_PropertyChanged);
        }
      }
    }

    private void UnRegisterPropertyChanged(IList items)
    {
      foreach (INotifyPropertyChanged item in items)
      {
        if (item != null)
        {
          item.PropertyChanged -= new PropertyChangedEventHandler(item_PropertyChanged);
        }
      }
    }

    private void item_PropertyChanged(object sender, PropertyChangedEventArgs e)
    {
      ///Launch an event Reset with name of property changed
      base.OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
    }
  }
}