using System.Collections;
using UnityEngine;
using TMPro;

[RequireComponent(typeof(BoxCollider))]
public class JumpBehaviour : MonoBehaviour
{
    [Header("Jump Settings")]
    public float jumpHeight;
    public float jumpWidth;
    public float jumpTime;

    [Header("Jump Condition")]
    public TextMeshProUGUI text;
    public float failJumpWidth;
    public float minSpeed;

    private Vector3 startPos;
    private Vector3 middlePos;
    private Vector3 endPos;
    private Vector3 endPosFail;

    private Vector3 endRot = new Vector3(45f, 0f, 0f);

    private Coroutine jumpRoutine;

    private BoxCollider boxCollider;

    private void Start()
    {
        GetComponent<BoxCollider>().isTrigger = true;

        text.text = minSpeed.ToString();

        RaycastHit hit;

        if (Physics.Raycast(new Vector3(transform.position.x, transform.position.y, transform.position.z + jumpWidth), Vector3.down, out hit))
        {
            endPos = hit.point;
            endPosFail = new Vector3(transform.position.x, endPos.y, transform.position.z + failJumpWidth);
        }
        else
        {
            Debug.LogError("Cant find EndPoint!");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            PlayerController player = other.gameObject.GetComponent<PlayerController>();
            player.rigid.constraints = RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            PlayerController player = other.gameObject.GetComponent<PlayerController>();

            player.jumpAudio.Play();

            PlayerVFX vfx = player.GetComponent<PlayerVFX>();

            Vector3 middlePosDown = (endPos - player.transform.position) * 0.5f;
            middlePos = new Vector3(middlePosDown.x, middlePosDown.y + jumpHeight, middlePosDown.z);

            Vector3 a = player.transform.position;
            Vector3 b = player.transform.position + middlePos;
            Vector3 c = endPos;

            if (player.currentSpeed < minSpeed)
            {
                c = endPosFail;
            }

            player.IsJumping = true;

            if (jumpRoutine == null)
            {
                jumpRoutine = StartCoroutine(Jump(player, a, b, c, jumpTime, vfx));
                player.rigid.constraints = RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;
            }
            else
            {
                Debug.Log("Cant start jump routine again!");
            }
        }

    }

    private IEnumerator Jump(PlayerController player, Vector3 a, Vector3 b, Vector3 c, float jumpTime, PlayerVFX vfx)
    {
        float elapsed = 0f;

        while (elapsed < jumpTime)
        {
            float t = elapsed / jumpTime;

            player.transform.position = Bezier.GetPoint(a, b, c, t);
            player.transform.rotation = Quaternion.Lerp(player.transform.rotation, Quaternion.Euler(endRot), t);

            //player.transform.forward = player.rigid.velocity + Bezier.GetDerivative(a, b, c, t);

            elapsed += Time.deltaTime;
            yield return null;
        }

        player.IsJumping = false;
        jumpRoutine = null;
        player.rigid.constraints = RigidbodyConstraints.FreezeRotation;

        player.rigid.velocity += Vector3.up * -2f;

        vfx.landingSparks.Play();
    }

    private void OnDrawGizmos()
    {
        RaycastHit hit;

        if (Physics.Raycast(new Vector3(transform.position.x, transform.position.y, transform.position.z + jumpWidth), Vector3.down, out hit))
        {
            endPos = hit.point;
        }
        else
        {
            Debug.LogError("Cant find EndPoint!");
        }

        Vector3 endPosFail = new Vector3(transform.position.x, endPos.y, transform.position.z + failJumpWidth);

        Vector3 middlePosDown = (endPos - transform.position) * 0.5f;
        middlePos = new Vector3(middlePosDown.x, middlePosDown.y + jumpHeight, middlePosDown.z);

        Gizmos.color = Color.green;
        Gizmos.DrawLine(transform.position, transform.position + middlePos);
        Gizmos.DrawLine(transform.position + middlePos, endPos);

        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position + middlePos, endPosFail);


    }

}
