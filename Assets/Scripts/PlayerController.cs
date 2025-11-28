using UnityEngine;
// Ya no necesitamos "using UnityEngine.InputSystem;"

public class PlayerController : MonoBehaviour
{
    public static PlayerController instance;

    private void Awake()
    {
        instance = this;
    }

    public CharacterController charCon;
    public float moveSpeed;
    // Eliminamos InputActionReference moveAction

    private Vector3 currentMovement;

    // Eliminamos InputActionReference lookAction
    private Vector2 rotStore;
    public float lookSpeed;

    public Camera theCam;

    public float minViewAngle = -60f, maxViewAngle = 60f;

    // Eliminamos InputActionReference jumpAction
    public float jumpPower;
    public float gravityModifier = 4f;

    public float runSpeed;
    // Eliminamos InputActionReference sprintAction

    public float camZoomNormal = 60f, camZoomOut = 70f, camZoomSpeed = 5f;

    public WeaponsController weaponCon;
    // Eliminamos InputActionReference shootAction, reloadAction, etc.

    public bool isDead;

    void Start()
    {
        // Bloquear el cursor en el centro de la pantalla
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        if (isDead) return;
        if (Time.timeScale == 0f) return;

        // --- MOVIMIENTO (WASD) ---
        float yStore = currentMovement.y;

        // Usamos los ejes clásicos "Horizontal" y "Vertical"
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");

        Vector3 moveForward = transform.forward * moveZ;
        Vector3 moveSideways = transform.right * moveX;

        // --- CORRER (Shift Izquierdo) ---
        if (Input.GetKey(KeyCode.LeftShift))
        {
            currentMovement = (moveForward + moveSideways) * runSpeed;

            if (currentMovement.x != 0 || currentMovement.z != 0) // Si nos movemos
            {
                theCam.fieldOfView = Mathf.Lerp(theCam.fieldOfView, camZoomOut, camZoomSpeed * Time.deltaTime);
            }
        }
        else
        {
            currentMovement = (moveForward + moveSideways) * moveSpeed;
            theCam.fieldOfView = Mathf.Lerp(theCam.fieldOfView, camZoomNormal, camZoomSpeed * Time.deltaTime);
        }

        // --- GRAVEDAD ---
        if (charCon.isGrounded)
        {
            yStore = -0.5f; // Un pequeño valor negativo para asegurar contacto con el suelo
        }

        currentMovement.y = yStore + (Physics.gravity.y * Time.deltaTime * gravityModifier);

        // --- SALTO (Espacio) ---
        if (Input.GetButtonDown("Jump") && charCon.isGrounded)
        {
            currentMovement.y = jumpPower;

            if (AudioManager.instance != null)
            {
                AudioManager.instance.PlaySFX(8);
            }
        }

        charCon.Move(currentMovement * Time.deltaTime);


        // --- CÁMARA (Ratón) ---
        // Input.GetAxis funciona directamente con el movimiento del mouse
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");

        // Ajustamos la sensibilidad
        rotStore.x += mouseX * lookSpeed * Time.deltaTime; // Rotación horizontal (Cuerpo)
        rotStore.y -= mouseY * lookSpeed * Time.deltaTime; // Rotación vertical (Cámara)

        rotStore.y = Mathf.Clamp(rotStore.y, minViewAngle, maxViewAngle);

        transform.rotation = Quaternion.Euler(0f, rotStore.x, 0f);
        theCam.transform.localRotation = Quaternion.Euler(rotStore.y, 0f, 0f);


        // --- DISPARO (Clic Izquierdo / Fire1) ---
        if (Input.GetButtonDown("Fire1"))
        {
            weaponCon.Shoot();
        }

        if (Input.GetButton("Fire1")) // Mantener presionado
        {
            weaponCon.ShootHeld();
        }

        // --- RECARGA (Tecla R) ---
        if (Input.GetKeyDown(KeyCode.R))
        {
            weaponCon.Reload();
        }

        // --- CAMBIAR ARMAS (Teclas Q y E) ---
        // Puedes cambiar KeyCode.E por lo que prefieras
        if (Input.GetKeyDown(KeyCode.E))
        {
            weaponCon.NextWeapon();
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            weaponCon.PreviousWeapon();
        }
    }
}