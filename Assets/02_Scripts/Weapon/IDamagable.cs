
/*
 인터페이스 : 상속받은 클래스에서 반드시 구현해야 하는 로직을 선언하는 규약

 C# - 다중 상속을 허용하지 않는다.
 */
public interface IDamagable
{
    public void TakeDamage(int damage);
}
