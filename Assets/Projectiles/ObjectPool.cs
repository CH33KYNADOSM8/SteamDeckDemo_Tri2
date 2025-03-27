using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    /*
     * Create the variables needed for the creation of the object pool.
     */ 

    public GameObject prefab;                                                               // Object inside of the pool
    public int poolSize = 15;                                                               // The amount of objects inside of the pool
     
    public Queue<IPooledObject> pool = new Queue<IPooledObject>();                          // Reference to the interface that contains some methods and other class references for the pool

    [SerializeField] private float objectLifetime = 0;                                      // Variable for the lifetime of the object if it hasn't already been destroyed


    // Initialise the pool at the start of the game so it is ready to be used whenever it is needed. 
    // Instantiate the objects into the world.
    // Set all of the objects in the pool to false so we can't see them in the game.
    // Add the objects to the pool.
    private void Start()
    {
        for (int i = 0; i < poolSize; i++)
        {
            IPooledObject obj = Instantiate(prefab).GetComponent<IPooledObject>();
            obj.gameObject.SetActive(false);
            obj.SetPool(this);
            pool.Enqueue(obj);
        }
    }
    /*
     * This method is structured so that instead of having to create a reference to find the object in the scene when the method is called
     * the reference will be made at the very start. In this case, I use the letter T to represent the reference to 'monobehaviour'. This
     * allows me to access any type of object as long as there is a reference at the start of the script.
     * 
     * I check to see if the object has been dequeued from the pool. 
     * If it has, we make that object visible.
     * We start the timer for the destruction of the object so that it won't stay spawned forever.
     */ 
    public T GetObject<T>() where T : MonoBehaviour
    {
        IPooledObject pooledObject = pool.Dequeue();
        T obj = (T)pooledObject;

        if (pooledObject.gameObject)
        {
            pooledObject.gameObject.SetActive(true);

            if (objectLifetime > 0)
                obj.StartCoroutine(CountObjectLifetime(pooledObject));

        }
        else
        {
            print("Pool is empty");
        }
        
        return obj;
    }


    // This will return the object to the pool and make sure that the object isn't visible anymore instead of destroying the object 
    public void ReturnObject(IPooledObject pooledObject)
    {
        pool.Enqueue(pooledObject);

        pooledObject.gameObject.SetActive(false);
    }

    // This will countdown to the time that I decide, in this case it is a variable set to 0.
    IEnumerator CountObjectLifetime(IPooledObject pooledObject)
    {
        yield return new WaitForSeconds(objectLifetime);

        ReturnObject(pooledObject);
    }
}
