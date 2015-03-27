using UnityEngine;
using System.Collections;

public interface ICmd {

	string [] Functions { get; }

	string Cmd(params string[] args);

}
