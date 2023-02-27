using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using System;
using Unity.VisualScripting;


//=================================================================
//	(1)�Ω�p��̤j�i�樫�d��,�é��I�粒����,�N�����P"����"�������ѼƦ^�k���
//	(2)�I��k��i�H����"����"
//=================================================================

public class EnemyPath : MonoBehaviour
{

	public GameObject chessBox; //�o�ӥΨө�ѽL��(ChessBox)
	public GameObject chessBoard; //�o�ӥΨө�ѽL��(BigChessBoard)
	public Button moveButton;
	private TestMapBlock testMapBlock;
	public List<TestMapBlock> results = new List<TestMapBlock>();


	public static Vector3 Position; //�ΨӰO���U�@�B����m
									//public static Vector3 PlayerPosition;	//���a��m

	private bool check = true;  //�ΨӧP�_�Y�@��O���O�w�g���L,�Y���L�h��false
	private bool monsterCheck = true;   //�ΨӧP�_�U�@��O�_���Ǫ�
	public static bool camera = false;  //�Ψӱ�����v���Ԫ�Ի�,false�Ԫ�,true�Ի�. �I��"����"�e�O�Ԫ�,�G���B��Ȭ�false
	public static bool button = true;   //�Ψӱ���Button�X�{���ɾ�,true�X�{,false����
	public static bool cancel = false;  //�ΨӨ�������԰����O,�b�I����NButton��,�ݱN���ܦ�true�~��o���@��

	public int m; //m = �i���ʼ�
	private int CanMove;  //�O���i���ʼ�,�Ĥ@���ݭn
	public static int index = 0;    //�s�J�}�C�Ϊ��޼�
	public int blockIndex;

	public static object Combine(string dataPath, string v)
	{
		throw new NotImplementedException();
	}

	public static int Count = 0;    //���X�}�Ҥ��e�Ϊ��޼�

	private int i = 0; //�j��p�ƥ�

	public static List<int> mCount = new List<int>();   //�ΨӰO���C�����ʹL��,�Ѿl��m��
	public static List<Vector3> ppp = new List<Vector3>();  //�ΨӰO���C�����ʹL�᪺�s��m


	//--------------------------------------------------------------------------------------------------

	void Start()
	{
		//PlayerPosition = this.transform.position;	//��PlayerPosition�x�s����ثe����m
		CanMove = m;    //��CanMove�]�w���̤j���ʼ�

		var results = new List<TestMapBlock>();
	}

	//--------------------------------------------------------------------------------------------------

	void Update()
	{
		if (ClickPosition.delete == true)   //�I����l����
			m = CanMove;    //�� m �Ȧ^�k�̤j��

		if (Input.GetMouseButton(1) && cancel == true)  //�I��k��,����"����"
		{
			cancel = false; //���@����N�ܦ�false,������I���y�����~
			camera = false; //�Ԫ���v��
			ClickPosition.delete = true;    //�Ndelete�]��true,�Ǧ��R���ѽL
											//.SetActive(false);    //���äj�ѽL
			Reset();    //���m�����Ѽ�,�^�k�I��"����"�e�����

		}


		//moveButton.onClick.AddListener(moveButtonActive);



	}

	//-----------------------------------------------------------------------------------------------





	void moveButtonActive()
	{
		if (button == true)
		{
			camera = true;  //�N���Y�Ի�
			moveButton.gameObject.SetActive(false); //��"����"Button����,������I��
			button = false;
			ClickPosition.delete = false;   //delete��false��,�ѽL��~��Q���(�J��)
											//chessBoard.SetActive(true); //��ܤj�ѽL
			StartEnemypath(); //�}�l�p��i�樫�d��
		}
	}
	//----------------------------------------------------------------------------------------------

	public List<TestMapBlock> StartEnemypath()
	{

		//results.Clear();
		for (i = 0; i < 500; i++)   //500�̦h�i�H���16��
		{

			//�Ĥ@��,�qPlayer��m�X�o,�̧Ǩ��W�U���k
			if (i == 0) //�Ĥ@��
			{
				//��_�l��m�s�J�}�C
				mCount.Insert(index, m);
				ppp.Insert(index, this.transform.position);
				index++;


				//�V�W��
				for (int j = 0; j < MonsterPosition.mIndex; j++)
				{
					if (((this.transform.position.z + 1) == MonsterPosition.monsterPosition[j].z) && (this.transform.position.x == MonsterPosition.monsterPosition[j].x))   //�p�G���U�ӭn���ʪ����榳�Ǫ�
						monsterCheck = false;   //�N��monsterCheck�ܦ�false
				}
				/*
				for (int j = 0; j < PartnerPosition.pIndex; j++)    //**�s���j��,�ΨӧP�_����
				{
					if (((this.transform.position.z + 1) == PartnerPosition.partnerPosition[j].z) && (this.transform.position.x == PartnerPosition.partnerPosition[j].x))   //�p�G���U�ӭn���ʪ����榳�Ǫ�
						monsterCheck = false;   //�N��monsterCheck�ܦ�false
				}
				*/

				if (monsterCheck == true)   //�p�G�Oture,��ܱ��U�ӭn���ʪ�����S���Ǫ�
				{
					Position = this.transform.position + new Vector3(0, 0, 1);  //���N��,�M���Position�O���s��m
					PathCount();    //�O���s��m,�H�ηs��m���Ѿl m(��ʤO) ��
					m = CanMove;    //�� m �^�k�̤j��

				}

				monsterCheck = true;    //�������@�Ӥ�V�N�^�k���


				//�V�U��
				for (int j = 0; j < MonsterPosition.mIndex; j++)
				{
					if (((this.transform.position.z - 1) == MonsterPosition.monsterPosition[j].z) && (this.transform.position.x == MonsterPosition.monsterPosition[j].x))   //�p�G���U�ӭn���ʪ����榳�Ǫ�
						monsterCheck = false;
				}
				/*
				for (int j = 0; j < PartnerPosition.pIndex; j++)//**�s���j��,�ΨӧP�_����
				{
					if (((this.transform.position.z - 1) == PartnerPosition.partnerPosition[j].z) && (this.transform.position.x == PartnerPosition.partnerPosition[j].x))   //�p�G���U�ӭn���ʪ����榳�Ǫ�
						monsterCheck = false;
				}
				*/

				if (monsterCheck == true)
				{
					Position = this.transform.position + new Vector3(0, 0, -1);
					PathCount();
					m = CanMove;

				}

				monsterCheck = true;


				//�V����
				for (int j = 0; j < MonsterPosition.mIndex; j++)
				{
					if (((this.transform.position.x - 1) == MonsterPosition.monsterPosition[j].x) && (this.transform.position.z == MonsterPosition.monsterPosition[j].z))   //�p�G���U�ӭn���ʪ����榳�Ǫ�
						monsterCheck = false;
				}
				/*
				for (int j = 0; j < PartnerPosition.pIndex; j++)//**�s���j��,�ΨӧP�_����
				{
					if (((this.transform.position.x - 1) == PartnerPosition.partnerPosition[j].x) && (this.transform.position.z == PartnerPosition.partnerPosition[j].z))   //�p�G���U�ӭn���ʪ����榳�Ǫ�
						monsterCheck = false;
				}
				*/

				if (monsterCheck == true)
				{
					Position = this.transform.position + new Vector3(-1, 0, 0);
					PathCount();
					m = CanMove;

				}

				monsterCheck = true;


				//�V�k��
				for (int j = 0; j < MonsterPosition.mIndex; j++)
				{
					if (((this.transform.position.x + 1) == MonsterPosition.monsterPosition[j].x) && (this.transform.position.z == MonsterPosition.monsterPosition[j].z))   //�p�G���U�ӭn���ʪ����榳�Ǫ�
						monsterCheck = false;
				}
				/*
				for (int j = 0; j < PartnerPosition.pIndex; j++)//**�s���j��,�ΨӧP�_����
				{
					if (((this.transform.position.x + 1) == PartnerPosition.partnerPosition[j].x) && (this.transform.position.z == PartnerPosition.partnerPosition[j].z))   //�p�G���U�ӭn���ʪ����榳�Ǫ�
						monsterCheck = false;
				}
				*/

				if (monsterCheck == true)
				{
					Position = this.transform.position + new Vector3(1, 0, 0);
					PathCount();
					m = CanMove;    // �̫�@�ӥi�H���ݭn,�]���ĤG���}�l��m,�O�q��~�}�C�̧����

				}

				monsterCheck = true;
				Count++;    //�C�����@���N+1,�ΥH�F��"�󴫥X�o�I"���ĪG

			}


			//��2~n��,���s�X�o�I,�W�U���k
			if (i != 0) //��2~n��
			{

				//�V�W��
				//���X�}�C�ثe�̫e�ݪ���,�N���g���L���Y�@�I�@���s�X�o�I,�çP�_�U�@�B����m�O�_���Ǫ�. �t�~,�p�G�s�X�o�I��m��<=0,��ܤ���A��
				for (int j = 0; j < MonsterPosition.mIndex; j++)
				{
					if (((ppp[Count].z + 1) == MonsterPosition.monsterPosition[j].z) && (ppp[Count].x == MonsterPosition.monsterPosition[j].x) && (mCount[Count] > 0))
						monsterCheck = false;
				}
				/*
				for (int j = 0; j < PartnerPosition.pIndex; j++)//**�s���j��,�ΨӧP�_����
				{
					if (((ppp[Count].z + 1) == PartnerPosition.partnerPosition[j].z) && (ppp[Count].x == PartnerPosition.partnerPosition[j].x) && (mCount[Count] > 0))
						monsterCheck = false;
				}
				*/
				if (monsterCheck == true)
				{
					Position = ppp[Count] + new Vector3(0, 0, 1);   //�q�s�X�o�I���W���@��,�M���Position�O�����ʫ᪺��m
					pppCheck(); //�ˬd�{�b�ձ����o��,���e�O�_���L
				}

				if (check == true && monsterCheck == true && (mCount[Count] > 0))   //�p�Gcheck = true,��ܱ��U�ӳo�B�٨S���L. monsterCheck = true,��ܱ��U�ӳo��S�Ǫ�
					PathCount();    //�O���s��m,�H�ηs��m���Ѿl m(��ʤO) ��

				check = true;   //��check�^�k���
				monsterCheck = true;    //��monsterCheck�^�k���



				//�V�U��
				for (int j = 0; j < MonsterPosition.mIndex; j++)
				{

					if (((ppp[Count].z - 1) == MonsterPosition.monsterPosition[j].z) && (ppp[Count].x == MonsterPosition.monsterPosition[j].x) && (mCount[Count] > 0))
						monsterCheck = false;
				}
				/*
				for (int j = 0; j < PartnerPosition.pIndex; j++)//**�s���j��,�ΨӧP�_����
				{

					if (((ppp[Count].z - 1) == PartnerPosition.partnerPosition[j].z) && (ppp[Count].x == PartnerPosition.partnerPosition[j].x) && (mCount[Count] > 0))
						monsterCheck = false;
				}
				*/
				if (monsterCheck == true)
				{
					Position = ppp[Count] + new Vector3(0, 0, -1);
					pppCheck();
				}

				if (check == true && monsterCheck == true && (mCount[Count] > 0))
					PathCount();

				check = true;
				monsterCheck = true;


				//�V����
				for (int j = 0; j < MonsterPosition.mIndex; j++)
				{
					if (((ppp[Count].x - 1) == MonsterPosition.monsterPosition[j].x) && (ppp[Count].z == MonsterPosition.monsterPosition[j].z) && (mCount[Count] > 0))
						monsterCheck = false;
				}
				/*
				for (int j = 0; j < PartnerPosition.pIndex; j++)//**�s���j��,�ΨӧP�_����
				{
					if (((ppp[Count].x - 1) == PartnerPosition.partnerPosition[j].x) && (ppp[Count].z == PartnerPosition.partnerPosition[j].z) && (mCount[Count] > 0))
						monsterCheck = false;
				}
				*/
				if (monsterCheck == true)
				{
					Position = ppp[Count] + new Vector3(-1, 0, 0);
					pppCheck();
				}

				if (check == true && monsterCheck == true && (mCount[Count] > 0))
					PathCount();

				check = true;
				monsterCheck = true;


				//�V�k��
				for (int j = 0; j < MonsterPosition.mIndex; j++)
				{
					if (((ppp[Count].x + 1) == MonsterPosition.monsterPosition[j].x) && (ppp[Count].z == MonsterPosition.monsterPosition[j].z) && (mCount[Count] > 0))
						monsterCheck = false;
				}
				/*
				for (int j = 0; j < PartnerPosition.pIndex; j++)//**�s���j��,�ΨӧP�_����
				{
					if (((ppp[Count].x + 1) == PartnerPosition.partnerPosition[j].x) && (ppp[Count].z == PartnerPosition.partnerPosition[j].z) && (mCount[Count] > 0))
						monsterCheck = false;
				}
				*/
				if (monsterCheck == true)
				{
					Position = ppp[Count] + new Vector3(1, 0, 0);
					pppCheck();
				}

				if (check == true && monsterCheck == true && (mCount[Count] > 0))
					PathCount();

				check = true;
				monsterCheck = true;


				//�Ĥ@��������,Count=1. �ĤG����}�l,�|�q��1�Ӧ�m�}�l���X�}�C��,�N�Yppp[1],mCount[1]
				//�ĤG��������,��Count+1,�U�@���~�|���X�}�C����2�Ӧ�m,�N�Yppp[2],mCount[2]
				//�ΥH�F��"�󴫷s�X�o�I"���ĪG
				Count++;

			}

			//index���s�J�}�C�Ϊ����ޭ�,�Y���X�}�C�Ϊ����ޭ�(Count)>=index,�����}�C�w�g�j������
			if (Count >= index)
				break;  //���X�j��

		}

		cancel = true;  //�o�ɭԫ�"�k��"�~��������ʪ��\��
		return results;
	}


	//-------------------------------------------------------------------------------------

	public void pppCheck() //�Ψ��ˬd�Y�@��O�_�w�g���L
	{
		for (int k = 0; k < index; k++) //�]�Mppp�}�C�̩Ҧ�����
		{
			if (Position == ppp[k]) //�p�G���U�ӭn�����@�B,�b�}�C�̤w�g���F(��ܴ��g���L)
			{
				check = false;  //��check�ܦ�false
				break;  //���X�j��
			}
		}
	}

	//--------------------------------------------------------------------------------------

	public void PathCount()    //�ΨӰO���B�p��C�@�檺�y�Ц�m�A�H�ΨC�@�檺 m(�Ѿl��ʤO) ��
	{
		//�b�s��m�W,�J���@�ӴѽL��
		//Position.x(�s��m��X�y��),Position.z(�s��m��Z�y��)
		//Instantiate(chessBox, new Vector3(Position.x, 0, Position.z), chessBox.transform.rotation);
		Vector3 hi = new Vector3(Position.x, 0, Position.z);

		foreach (var gameObject in TestMapManager.testMapBlocks)
		{
			if (gameObject.transform.position == hi)
			{
				//gameObject.transform.GetChild(0).gameObject.SetActive(true);
				print(gameObject.transform.GetChild(0).gameObject);
				results.Add(gameObject);
			}
		}

		m = mCount[Count] - 1;  //��ʼ�-1(mCount[Count]�O���ʫe��m��,�]��ppp[Count]�O���ʫe����m)
		mCount.Insert(index, m);    //��o�����ʹL��Ѿl��m��,�s�J�}�C
		ppp.Insert(index, Position);    //��o�����ʹL�᪺�s��m,�s�J�}�C
		index++;    //���x�s�Ϊ��޼�+1
	}

	//---------------------------------------------------------------------------------------

	void Reset()
	{
		moveButton.gameObject.SetActive(true);
		button = true;//�N"����"Button��ܥX��
		index = 0;  //�s�Jppp[]�Ϊ����ޭ��k0(���)
		Count = 0;  //���Xppp[]�Ϊ����ޭ��k0(���)
		ppp.Clear();    //�M���x�s�樫�d�򪺰}�C
		mCount.Clear(); //�M���x�s m �Ȫ��}�C
	}

	//---------------------------------------------------------------------------------------

}



