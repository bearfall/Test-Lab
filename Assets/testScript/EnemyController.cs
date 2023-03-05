using UnityEngine;
using System.Collections;
using UnityEngine.UI;

//=================================================================
//	�Ω󥭷Ʋ��ʨ���,�é󲾰ʧ�����,�N�����P"����"�������ѼƦ^�k���
//=================================================================

public class EnemyController : MonoBehaviour
{
	private int u = 2;
	private TestCharacter testCharacter;
	EnemyPath enemyPath;


	public int MoveSpeed;   //�Ψӽվ㲾�ʳt��,�ƭȶV�j�V��
							//public Button moveButton;
							//private CharacterStats characterStats;
							//private GameObject target;
							//private bool _canBeAttack;



	//aaa[targetChess - 2] = �_�l��m���U�@�Ӧ�m,�]��aaa[targetChess]�O�Ū�,aaa[targetChess-1]�O�_�l��m
	//private int i = 2;


	//------------------------------------------------------------------------------------
	private void Awake()
	{
		//characterStats = GetComponent<CharacterStats>();

	}

	private void Start()
	{
		//characterStats.MaxHealth = 30;
		testCharacter =gameObject.GetComponent<TestCharacter>();
		enemyPath = gameObject.GetComponent<EnemyPath>();
	}


	void Update()
	{
		if (testCharacter.enemyChose == true)    //���U�ƹ�����,�ӥB�P�w�O�I���FChessBox
		{
			StartCoroutine(EnemyMove()); //�Ұʭp�ƾ�,�Ω�move()
			testCharacter.enemyChose = false;    //�O�ƹ����䥢��,����ʤ������I��,�y���p����~

		}

	}
	/*
    private void OnTriggerstay(Collider enemy)
    {
		target = enemy.gameObject;
		print(target.name);
    }
	*/
	//------------------------------------------------------------------------------------

	public IEnumerator EnemyMove()
	{
		while (true)
		{

			//�p��ؼ��I�M�{�b���y�Юt(�o�O�@�ӦV�q)
			Vector3 distance = testCharacter.Enemyaaa[testCharacter.enemyTargetChess - u] - this.transform.position;
			//�N�y�Юt���⦨����(�¶q)
			float len = distance.magnitude;

			distance.Normalize();   //�N�y�Юt�ഫ�����V�q����ƫ��A(�V�q)


			//���k�������
			if (distance.x > 0.1f)
				this.transform.eulerAngles = new Vector3(0, 90, 0);

			//�����������
			if (distance.x < -0.1f)
				this.transform.eulerAngles = new Vector3(0, -90, 0);

			//���W�������
			if (distance.z > 0.9f)
				this.transform.eulerAngles = new Vector3(0, 0, 0);

			//���U�������
			if (distance.z < -0.9f)
				this.transform.eulerAngles = new Vector3(0, 180, 0);



			//�p�G�ؼ��I�P�{�b����m,�Z���C��o�@�V������
			if (len <= (distance.magnitude * Time.deltaTime * 2))   //distance.magnitude�O�@�ӯ¶q
			{
				//��{�b��m�j��]�w���ؼЦ�m
				this.transform.position = testCharacter.Enemyaaa[testCharacter.enemyTargetChess - u];
				u++;    //���ޭ�+1
				if (testCharacter.enemyTargetChess - u < 0)  //aaa[-1]���s�b
				{
					u = 2;  //�N i �^�k���
					break;  //���X�j��
				}

			}

			//Delay time ���:��
			yield return new WaitForSeconds(1 / MoveSpeed);
			//�H�ɶ����ʥثe�y��
			this.transform.position = this.transform.position + (distance * Time.deltaTime * 2); //distance�O�@�ӦV�q
		}


		//TestCharacter.delete = false;   //��Ω�R��chessbox��bool��,�^�kfalse(���)

		enemyPath.index = 0; //�s�Jppp[]�Ϊ����ޭ��k0(���)
		enemyPath.Count = 0; //���Xppp[]�Ϊ����ޭ��k0(���)
		testCharacter.mSave = 0;    //�Ȧs�̤j m �Ȫ��ܼ��k0(���)
		testCharacter.enemyTargetChess = 0;  //�s�J�M���Xaaa[]�Ϊ����ޭ��k0(���)

		enemyPath.ppp.Clear();   //�M���x�s�樫�d�򪺰}�C
		enemyPath.mCount.Clear();    //�M���x�s m �Ȫ��}�C
		testCharacter.Enemyaaa.Clear(); //�M���x�s�̵u�樫���|���}�C

		//moveButton.gameObject.SetActive(true);
		enemyPath.button = true;//�N"����"Button��ܥX��

		//TestCharacter.ChessBoard = false; //���ʧ�����,�N���äj�ѽL��bool�^�k���(false)


	}

}
