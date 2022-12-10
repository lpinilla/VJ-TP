using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RankingManager : MonoBehaviour
{
    [SerializeField] private GameObject _elementPrefab;
    [SerializeField] private Transform _elementsGridTransform;

    private List<RankingModel> _elements = new List<RankingModel>();
    private List<string> _dummyNames;

    // private Database _db;

    private void Start()
    {
        /* Database constructor */
        // _db = new Database();

        /* init de la lista */
        _dummyNames = new List<string> { 
            "Matias", "Maximiliano", "Pablo", "Carlos", "Leandro"};

        for (int i = 0; i < 5; i++)
        {
            _elements.Add(new RankingModel (
                _dummyNames[Random.Range(0, _dummyNames.Count)],
                Random.Range(0, 50000)
            ));
            
        }
       // _elements = _db.GetAllRecords();

        /* Ordenamiento por puntaje */
        _elements.Sort((element1, element2) => element2.Score.CompareTo(element1.Score));

        /* Instanciando elemento de la ui */
        foreach (var element in _elements)
        {
            var rankingElement = 
                Instantiate(_elementPrefab, _elementsGridTransform).GetComponent<RankingElement>();
            rankingElement.SetModel(element.Name, element.Score);
        }
    }
}
